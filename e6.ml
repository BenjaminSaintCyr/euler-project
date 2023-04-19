let (--) i j = 
  let rec aux n acc =
    if n < i then acc else aux (n-1) (n :: acc)
  in aux j []

let rec int_pow base exponent =
  if exponent <= 0 then 1
  else base * int_pow base (exponent - 1)

let square x = int_pow x 2

let () =
  let max = 100 in
  let sum_square =
    1--max
    |> List.map square
    |> List.fold_left Int.add 0
  in
  let square_sum = 
    1--max
    |> List.fold_left Int.add 0
    |> square
  in
  print_int (square_sum - sum_square)
