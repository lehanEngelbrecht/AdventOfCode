open System.IO
open System.Text.RegularExpressions

type Cubes =
    {
        Blue : int
        Red : int
        Green : int
    }

module Cubes =
    let sumSpecificColor (color : string) (strArray : string array): int =
        let rx = Regex(@"[0-9]+", RegexOptions.Compiled)
        strArray
        |> Array.filter (fun x -> x.ToLower().Contains(color))
        |> Array.map rx.Matches
        |> Array.map (fun e -> e.[0] |> string |> int)
        |> Array.sum

    let populateCubes (stringArr : string array) : Cubes =

        let allBlue =
            stringArr
            |> sumSpecificColor "blue"
            
        let allRed =
            stringArr
            |> sumSpecificColor "red"
        let allGreen =
            stringArr
            |> sumSpecificColor "green"
        {
            Blue = allBlue
            Red = allRed
            Green = allGreen
        }

let calculateRespectiveCubes (str : string array) =
    let rx = Regex(@"[0-9]+", RegexOptions.Compiled)
    let cubesArray =
                     str.[1].Split(';')
                     |> Array.map (fun elem -> elem.Trim())
                     |> Array.map (fun entryStr -> entryStr.Split(','))
                     |> Array.map Cubes.populateCubes
    let isValidRoundQ1 =
                     str.[1].Split(';')
                     |> Array.map (fun elem -> elem.Trim())
                     |> Array.map (fun entryStr -> entryStr.Split(','))
                     |> Array.map Cubes.populateCubes
                     |> Array.forall (fun x -> (x.Red <= 12 && x.Green <= 13 && x.Blue <= 14))
    // 12 red cubes, 13 green cubes, and 14 blue cubes
    let gameIDMatch = rx.Matches(str.[0])
    let id =
        if isValidRoundQ1 then
            gameIDMatch.[0] |> string |> int
        else
            0
    let maxBlue =
        cubesArray
        |> Array.maxBy (fun x -> x.Blue)
    let maxRed =
        cubesArray
        |> Array.maxBy (fun x -> x.Red)
    let maxGreen =
        cubesArray
        |> Array.maxBy (fun x -> x.Green)
    [|id; (maxBlue.Blue*maxGreen.Green*maxRed.Red)|]

let readFile (fileName : string) =
    let tempInput = [|"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";
                    "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue";
                    "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red";
                    "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red";
                    "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"|]
    let rows = File.ReadAllLines fileName
    let q1Answer = rows
                    |> Array.map (fun str -> str.Split(':'))
                    |> Array.map calculateRespectiveCubes
                    |> Array.sumBy (fun x -> x |> Array.head)
    let q2Answer =
                 rows
                 |> Array.map (fun str -> str.Split(':'))
                 |> Array.map calculateRespectiveCubes
                 |> Array.sumBy (fun x -> x.[1])

    printfn "Q1 answer day 2: %d" q1Answer
    printfn "Q2 answer day 2: %d" q2Answer

[<EntryPoint>]
let main argv =
    readFile argv.[0]
    0
