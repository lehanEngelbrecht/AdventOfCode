open System.IO

let testSurroundings (strArray : string array) (i : int) (specificString : string) =
    
    printfn "i -> %d" i


let solveQ1 (strArray : string array) =
    strArray
    |> Array.iteri (fun i str -> testSurroundings strArray i str)

    printfn "Nothing"

[<EntryPoint>]
let main argv =
    let fileName = "/Users/lehanengelbrecht/My_Code/Advent/2023_Q3/input.txt"
    let testInput = [|"467..114..";
                    "...*......";
                    "..35..633.";
                    "......#...";
                    "617*......";
                    ".....+.58.";
                    "..592.....";
                    "......755.";
                    "...$.*....";
                    ".664.598.."|]
    let rows = File.ReadAllLines fileName
    testInput
    |> solveQ1
    0
