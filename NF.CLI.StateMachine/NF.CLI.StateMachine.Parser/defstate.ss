(define (map fn ls)
 (define loop
   (lambda (ls acc)
     (if (null? ls)
       acc
       (loop (cdr ls)
             (append acc (list (fn (car ls))))))))
 (loop ls '()))


(define (apply-argument event ls)
 (define xx
  (lambda (x)
   (AddArgument event x)))
 (map xx ls))


(define-macro def-event
  (lambda args
    (define x (name (car args)))
    (define rst (cdr args))
    (define args (map name (car rst)))
    (if (null? (cdr rst))
      (define nextstate "")
      (define nextstate (name (car (cdr rst)))))

    `(let ((event (define-event ,x)))
       (apply-argument event ',args)
       (ConnectNextState event ,nextstate)
       event)))


(define (apply-event state ls)
 (define xx
  (lambda (x)
   (AddEvent state x)))
 (map xx ls))

; (def-state GenerateColor
;   (EvtNext (color) ValidateColor)
; )
(define-macro def-state
   (lambda args
     (define x (name (car args)))
     (define bodies (cdr args))
     (define xxx (lambda (lst)
      (cons 'def-event lst)))
     (define xxxx (map xxx bodies))
     `(let ((state (define-state ,x)))
        (apply-event state (list ,@xxxx))
        state)))

(define (apply-state fsm ls)
 (define xx
  (lambda (x)
   (AddState fsm x)))
 (map xx ls))

;(def-fsm HelloFSM
;  (GenerateColor (EvtNext color) ValidateColor)
;  (ValidateColor (EvtInvalid) GenerateColor)
;  (ValidateColor (EvtValid color) DisplayColor)
;
;  (DisplayColor (EvtNext) GenerateColor)
;  )
(define-macro def-fsm
   (lambda args
     (define x (name (car args)))
     (define bodies (cdr args))
     (define xxx (lambda (lst)
      (let ((a (car lst))
            (b (cdr lst)))
        (cons 'def-state lst))))
     (define xxxx (map xxx bodies))
     `(let ((fsm (define-fsm ,x)))
        (apply-state fsm (list ,@xxxx))
        fsm)))
