(define-macro first
  (lambda args
     `(car ,@args)))

(define-macro rest
  (lambda args
     `(cdr ,@args)))
	 
(define-macro second
  (lambda args
     `(first (rest ,@args))))

(define (map fn lst)
 (define loop
   (lambda (ls acc)
     (if (null? ls)
       acc
       (loop (rest ls)
             (append acc (list (fn (first ls))))))))
 (loop lst '()))

(define-macro when
  (lambda args
   (define fst# (first args))
   (define body# (rest args))
    `(if ,fst#
       ,@body#)))

(define-macro foreach
  (lambda args
    (define item# (first args))
    (define lst# (second args))
    (define body# (rest (rest args)))
    `(begin
      (define __internal##
       (lambda (ls)
	     (when (not (null? ls))
           (let ((,item# (first ls))
                 (rst# (rest ls)))
             ,@body#
             (__internal## rst#)))))
      (__internal## ,lst#))))

(define-macro def-event
  (lambda args
    (define event-name (name (first args)))
    (define rst (rest args))
    (define args (map name (car rst)))
    (if (null? (cdr rst))
      (define next-state-name "")
      (define next-state-name (name (car (cdr rst)))))

    `(let ((event (define-event ,event-name)))
	   (foreach x ',args
	     (AddArgument event x))
       (ConnectNextState event ,next-state-name)
       event)))

(define-macro def-state
   (lambda args
     (define state-name (name (first args)))
     (define bodies (rest args))
     (define xxx (lambda (lst)
      (cons 'def-event lst)))
     (define xxxx (map xxx bodies))
     `(let ((state (define-state ,state-name)))
        (foreach x (list ,@xxxx)
		  (AddEvent state x))
        state)))

(define-macro def-fsm
   (lambda args
     (define fsm-name (name (first args)))
     (define bodies (rest args))
     (define xxx 
      (lambda (lst)
        (let ((fst (first lst))
              (rst (rest lst)))
          (cons 'def-state lst))))
     (define xxxx (map xxx bodies))
     `(let ((fsm (define-fsm ,fsm-name)))
        (foreach x (list ,@xxxx)
          (AddState fsm x))
        fsm)))
