let wordsplit (s: string) =
  (string s)
    .Split([|' ';'\n';'\r';'\t'|],
           System.StringSplitOptions.RemoveEmptyEntries)

let readLines (reader: System.IO.TextReader) =
  let rec loop () = seq {
      let line = reader.ReadLine()
      if line <> null then
        yield line
        yield! loop ()
  }
  loop ()

let filename =
  match System.Environment.GetCommandLineArgs() with
  | [| _; filename |] ->
    filename
  | _                 ->
    __SOURCE_DIRECTORY__
    + "/../data/huwikisource-latest-pages-meta-current.xml"

let wordcount filename =
  System.IO.File.ReadLines filename
  |> Seq.collect wordsplit
  |> Seq.countBy id
  |> Seq.sortBy (fun (w, i) -> -i, w)
  |> Seq.take 10
  |> Seq.iter (fun (w, i) -> printfn "%s\t%i" w i)

wordcount filename

//readLines stdin
//|> collect wordsplit
//|> wordcount
//|> print
