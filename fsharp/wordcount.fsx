let wordcount (s: string) =
  (string s).Split([|' ';'\n';'\r';'\t'|], System.StringSplitOptions.RemoveEmptyEntries)
  |> Seq.countBy id
  |> Seq.sortBy (fun (w, i) -> -i, w)
  |> Seq.iter (fun (w, i) -> printfn "%s\t%i" w i)

do
  use reader = stdin
  wordcount (reader.ReadToEnd())