open System
open System.IO
open System.Collections.Generic
 
let wordsplit (s: string) =
  (string s)
    .Split([|' ';'\n';'\r';'\t'|],
           StringSplitOptions.RemoveEmptyEntries)

let increment key (dict: Dictionary<string, int>) =
  match dict.TryGetValue key with
  | true, v -> dict.[key] <- v + 1
  | false, _ -> dict.Add(key, 1)

let comparer =
  { new IComparer<KeyValuePair<string, int>> with
      member __.Compare((KeyValue(k1, v1)), (KeyValue(k2, v2))) =
        match compare v2 v1 with
        | 0 -> compare k1 k2
        | i -> i }

let sort hs =
  let words = Seq.toArray hs
  System.Array.Sort(words, comparer)
  words

let wordcount lines =
  let dict = Dictionary<string, int>(10240, HashIdentity.Structural)

  for line in lines do
    for word in wordsplit line do
      increment word dict

  for KeyValue(k, v) in Seq.truncate 10 (sort dict) do
    printfn "%s\t%i" k v

let readLines (reader: System.IO.TextReader) =
  let rec loop () = seq {
      let line = reader.ReadLine()
      if line <> null then
        yield line
        yield! loop ()
  }
  loop ()

wordcount <| readLines stdin
