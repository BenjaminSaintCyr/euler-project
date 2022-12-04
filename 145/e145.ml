(* utils *)
let rec gcd a b =
  if b = 0 then a else gcd b (a mod b)

let coprime a b = gcd a b = 1

let phi n =
  let rec count_coprime acc d =
    if d < n then
      count_coprime (if coprime n d then acc + 1 else acc) (d + 1)
    else acc
  in
  if n = 1 then 1 else count_coprime 0 1

(* solution *)
let rev n = 
    let rec loop acc = function
      | 0 -> acc
      | n -> loop (acc * 10 + n mod 10) (n / 10) in 
    loop 0 n

let rec odd_digits = function
    | 0 -> true
    | n when (n mod 10) mod 2 != 1 -> false
    | n -> odd_digits (n / 10)

let () = 
  let count = ref 120 in
  for n = 1000 to 1_000_000_000 do
    if n mod 10 != 0 && n + rev n |> odd_digits
    then count := !count + 1
  done;
  print_int !count
