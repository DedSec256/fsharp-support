match () with
| a
| (_ as a) & _ -> ()

match () with
| a
| _ & (_ as a) -> ()

match () with
| a
| _ & (_ as a) & _ -> ()
