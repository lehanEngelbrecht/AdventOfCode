open System
open System.IO
open System.Text.RegularExpressions


type Digits =
    {
        first : string
        last : string
        total : int
    }
    


module Digits =
    let populateDigits (str: string): Digits=
        let aa = str
                 |> Seq.filter (fun s -> s |> Char.IsDigit)
                 |> Seq.toArray
        let first1 = Char.ToString aa.[0]
        let last1 = Char.ToString aa.[aa.Length-1]
        let someValue = first1 + last1
        {
            first = first1
            last = last1
            total = someValue |> int
        }

    let stringToDigit inputString =
        match inputString with 
            | "one" -> "1"
            | "two" -> "2"
            | "three" -> "3"
            | "four" -> "4"
            | "five" -> "5"
            | "six" -> "6"
            | "seven" -> "7"
            | "eight" -> "8"
            | "nine" -> "9"
            | _ -> inputString
        
        
    let question2Digits (str: string): Digits =
        (*printfn "Entries are: %A" str*)
        let rx = Regex("one|two|three|four|five|six|seven|eight|nine|1|2|3|4|5|6|7|8|9", RegexOptions.Compiled)
        let pattern = "one|two|three|four|five|six|seven|eight|nine|1|2|3|4|5|6|7|8|9"
        let matchedR = rx.Matches(str)
                       |> Seq.toArray
        printfn "Array is: %A" matchedR
        let firstValue = matchedR[0]
                         |> string
                         |> stringToDigit
        let matchedRLast =
            Regex.Matches(str, pattern, RegexOptions.RightToLeft)
            |> Seq.toArray
        let lastValue = matchedRLast[0]
                        |> string
                        |> stringToDigit
        let someValue = firstValue + lastValue
        {
            first = firstValue
            last = lastValue
            total =  someValue |> int
        }

let readFile filePath =
    let rows = File.ReadAllLines filePath 
    let answer = rows
                 |> Array.map Digits.populateDigits
                 |> Array.sumBy (fun x -> x.total)

    let users = [|"two1nine";
                   "eightwothree";
                   "abcone2threexyz";
                    "xtwone3four";
                    "4nineeightseven2";
                    "zoneight234";
                    "7pqrstsixteen"|]

    // break up the string into a sequence
    let q2Answer = rows
                |> Array.map Digits.question2Digits
                |> Array.sumBy (fun elem -> elem.total)

    printfn "Question 1 answer is: %d" answer
    printfn "Question2 answer is: %d" q2Answer


[<EntryPoint>]
let main argv =
    readFile argv.[0]
    0
