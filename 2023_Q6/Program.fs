open System.IO
open System
open Microsoft.FSharp.Core

let getDistanceArray (size : int) =
    // printfn "Incoming size: %d" size
    let temp1Arr = [| for i in 1..size do
                          i |]
    let temp2Arr = Array.rev temp1Arr
    let sumArr = Array.map2 (*) temp1Arr temp2Arr

    sumArr

let getDistanceArrayQ2 (size : bigint) =
    let temp1Arr = [| for i in 1I..size do
                          i |]
                    |> Array.toSeq

    let temp2Arr = Seq.rev temp1Arr
    let sumArr = Seq.map2 (*) temp1Arr temp2Arr

    sumArr

let calculateWins (distance : int) (possibleTravels : int array) =
    let wins =
        possibleTravels
        |> Array.filter (fun x -> x > distance)
        |> Array.length
    wins

let solveProblem (times: string) (distances : string) =
    let timesArr =
        times.Split(':')
        |> Array.map (fun x -> x.Trim())
    let justTimes =
        timesArr.[1].Split(" ")
        |> Array.filter (fun x -> (String.IsNullOrEmpty(x) |> not))
        |> Array.map (fun x -> x |> int)

    let distanceArr =
        distances.Split(':')
        |> Array.map (fun x -> x.Trim())

    let justDistances =
        distanceArr.[1].Split(" ")
        |> Array.filter (fun x -> (String.IsNullOrWhiteSpace(x) |> not))
        |> Array.map (fun x -> x |> int)

    let canTravel =
        justTimes
        |> Array.map (fun x -> (x-1) |> getDistanceArray)

    let answerQ1 =
        canTravel
        |> Array.mapi (fun i x -> x |> (calculateWins justDistances.[i]))
        |> Array.reduce (fun acc ele -> acc * ele)
    printfn "Answer for Q1: %A" answerQ1

let solveProblem2 (times : string) (distance : string)=
    let justTimes =
        times.Split(':')
        |> Array.filter (fun x -> String.IsNullOrWhiteSpace(x) |> not)
    let time =
        justTimes.[1].Split(' ')
        |> Array.reduce (fun acc elem -> acc + elem)
        |> int64

    let justDistance =
        distance.Split(':')
        |> Array.filter (fun x -> String.IsNullOrWhiteSpace(x) |> not)
    let distance =
        justDistance.[1].Split(' ')
        |> Array.reduce (fun acc elem -> acc + elem)
        |> int64

    let timeBig = bigint(time)
    let distanceBig = bigint(distance)


    let answerQ2 =
        (getDistanceArrayQ2 (timeBig-bigint(1)))
        |> Seq.toArray
        |> Seq.filter (fun x -> x > distanceBig)
        |> Seq.length

    printfn "Answer Q2: %A" answerQ2

[<EntryPoint>]
let main argv =
    let testInput = [|"Time:      7  15   30";
                    "Distance:  9  40  200"|]
    // I run from the terminal so the fileName works, if you use a
    // IDE runner the fileName has to be replaced with a abs filePath
    let rows = File.ReadAllLines "input.txt"
    let times = rows.[0]
    let distances = rows.[1]
    solveProblem times distances
    solveProblem2 times distances
    0
