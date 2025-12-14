module App

open System
open System.IO
open System.Xml
open System.Xml.XPath
open System.Xml.Xsl

open Newtonsoft.Json

open TteLcl.Moustample

open ColorPrint
open CommonTools

type private Options = {
  TemplateFile: string
  DataFile: string
  OutputFile: string
}

let private runApp o =
  ecp $"Loading \fg{o.TemplateFile}\f0."
  let template =
    let templateText = File.ReadAllText(o.TemplateFile)
    templateText |> Template.Parse
  let context = new TemplateRenderContext()
  if o.OutputFile |> String.IsNullOrEmpty then
    ecp "----- \fyRendering to console\f0 -----"
    template.Render(context, Console.Out)
    ecp "----- \fy--------------------\f0 -----"
  else
    do
      use w = o.OutputFile |> startFile
      template.Render(context, w)
    o.OutputFile |> finishFile
  0

let run args =
  let rec parseMore o args =
    match args with
    | "-v" :: rest ->
      verbose <- true
      rest |> parseMore o
    | "--help" :: _
    | "-h" :: _ ->
      None
    | "-t" :: file :: rest ->
      rest |> parseMore {o with TemplateFile = file}
    | "-d" :: file :: rest ->
      rest |> parseMore {o with DataFile = file}
    | "-o" :: file :: rest ->
      rest |> parseMore {o with OutputFile = file}
    | [] ->
      if o.TemplateFile |> String.IsNullOrEmpty then
        cp "\foNo template file (\fg-t\fo) given\f0."
        None
      else
        o |> Some
    | x :: _ ->
      cp $"\frUnrecognized argument \f0'\fy{x}\f0'"
      None
  let oo = args |> parseMore {
    TemplateFile = null
    DataFile = null
    OutputFile = null
  }
  match oo with
  | Some(o) ->
    o |> runApp
  | None ->
    cp ""
    Usage.usage "scc"
    1






