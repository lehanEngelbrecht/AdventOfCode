open System.IO
open System

let countMatch (numbers : string array) =
    let stringWinningNumbers =
        numbers.[1].Split("|")
        |> Array.head
    let stringMyNums =
        numbers.[1].Split("|")
        |> Array.last
    let arrayWinningNumbers =
        stringWinningNumbers.Trim().Split(" ")
    let matchingNums =
        stringMyNums.Trim().Split(" ")
        |> Array.filter (fun x -> String.IsNullOrWhiteSpace(x) |> not)
        |> Array.filter (fun elem -> Array.contains elem arrayWinningNumbers)

    (matchingNums.Length-1)

[<EntryPoint>]
let main argv =
    let testInput = [|"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53";
                    "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19";
                    "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1";
                    "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83";
                    "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36";
                    "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"|]
    let rows = File.ReadAllLines "/Users/lehanengelbrecht/My_Code/Advent/2023_Q4/input.txt"
    let q1Answer =
             rows
             |> Array.map (fun x -> x.Split(": "))
             |> Array.map countMatch
             |> Array.filter (fun elem -> elem >= 0)
             |> Array.sumBy (fun i -> (pown 2 i))

    let matchesPerGame =
             rows
             |> Array.map (fun x -> x.Split(": "))
             |> Array.map countMatch
             |> Array.map (fun elem -> elem + 1)
    let tempArray = [| for i in 0..(matchesPerGame.Length-1) do
                           1 |]
    for i in 0..tempArray.Length-1 do
        let tempElem = matchesPerGame.[i]
        let mutable scale = tempArray.[i]
        while scale > 0 do
            for j in (i+1)..(i+tempElem) do
                tempArray.[j] <- tempArray.[j] + 1
            scale <- scale - 1

    printfn "Q1 answer day 4: %d" q1Answer
    printfn "Q2 answer day 4: %d" (tempArray |> Array.sum)
    0
