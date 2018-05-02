(def-fsm HelloFSM
  (GenerateColor (EvtNext (color) ValidateColor))

  (ValidateColor (EvtInvalid () GenerateColor))
  (ValidateColor (EvtValid (color) DisplayColor))

  (DisplayColor (EvtNext () GenerateColor))
)
