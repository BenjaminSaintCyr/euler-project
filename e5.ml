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

let prime : int -> bool
  = fun n ->
    match n with
      0 -> false
    | 1 -> false
    | _ -> let a = (n - 1) in
      let rec checkZero a n =
        match a with
          1 -> true
        | _ -> match n mod a with
            0 -> false
          | _ -> checkZero (a - 1) n
      in
      checkZero a n

let greatest_prime_divider n =
  let rec loop i =
    if i = 1 || (n mod i = 0 && prime i)
    then i
    else loop (i - 1)
  in
  loop n

let (--) i j = 
  let rec aux n acc =
    if n < i then acc else aux (n-1) (n :: acc)
  in aux j []

(* solution *)
let brute_force max =
  let range = 2--max in
  let sat i =
    List.map (fun x -> i mod x = 0) range
    |> List.fold_left (&&) true
  in
  let rec loop i =
    if sat i then i
    else loop (i+1)
  in
  loop 2

let itt_sol max =
  let range = 2--max in
  let primes = List.filter prime range in
  let prod = List.fold_left Int.mul 1 primes in
  let rec loop result = function
    | hd :: tl ->
      let result =
        if result mod hd = 0 then result
        else
          result * (greatest_prime_divider hd)
      in
      loop result tl
    | [] -> result
  in
  loop prod range

(* 232792560 *)
let () =
  itt_sol 20
  |> print_int
