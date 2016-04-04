open System
open System.IO
open System.Collections.Generic

let wordsplit (s: string) =
  (string s)
    .Split([|' ';'\n';'\r';'\t'|],
           StringSplitOptions.RemoveEmptyEntries)

let increment key (hs: Dictionary<string, int>) =
  match hs.TryGetValue key with
  | true, v -> hs.[key] <- v + 1
  | false, _ -> hs.Add(key, 1)

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

let wordcount filename =
  let hs = Dictionary<string, int>(10240, HashIdentity.Structural)

  for line in File.ReadLines(filename) do
    for word in wordsplit line do
      increment word hs

  for KeyValue(k, v) in Array.truncate 10 (sort hs) do
    printfn "%s\t%i" k v

let filename =
  match System.Environment.GetCommandLineArgs() with
  | [| _; filename |] ->
    filename
  | _                 ->
    __SOURCE_DIRECTORY__
    + "/../data/huwikisource-latest-pages-meta-current.xml"

wordcount filename
