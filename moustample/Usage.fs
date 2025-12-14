// (c) 2025  ttelcl / ttelcl
module Usage

open CommonTools
open ColorPrint

let usage focus =
  cp "\foText templating tool\f0"
  cp ""
  cp "\fomoustample \fg-t \fctemplate.txt \fg-d \fcdata.json \f0[\fg-o \fcoutputfile\f0]"
  cp "   Apply the \fomoustample\f0 template \fctemplate.txt\f0 to the data \fcdata.json\f0."
  cp "\fg-preparse          \f0Debug mode: only pre-parse the template, emit placeholders for instructions"
  cp "\fg-v                 \f0Verbose mode"



