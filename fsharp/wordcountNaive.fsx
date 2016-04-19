let wordsplit (s: string) =
  (string s)
    .Split([|' ';'\n';'\r';'\t'|],
           System.StringSplitOptions.RemoveEmptyEntries)

let wordcount lines =
  lines
  |> Seq.collect wordsplit
  |> Seq.countBy id
  |> Seq.sortBy (fun (w, i) -> -i, w)
  |> Seq.take 10
  |> Seq.iter (fun (w, i) -> printfn "%s\t%i" w i)

let readLines (reader: System.IO.TextReader) =
  let rec loop () = seq {
      let line = reader.ReadLine()
      if line <> null then
        yield line
        yield! loop ()
  }
  loop ()

wordcount <| readLines stdin
