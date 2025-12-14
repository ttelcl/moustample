# The moustample templating language

## High level view

At the highest level, moustample templates are plain text files that
contain a mix of plain text and _template instructions_ wrapped in
`{{` and `}}`.
Or more pedantically: wrapped starting with two or more `{` characters
and ending with the ocurrence of an equal number of `}` characters
_not immediately followed by any more `}` characters_.

What those '_template instructions_' do is what most of the
rest of this document is all about. But at the first parsing pass,
_moustample_ doesn't care.

As an example, the template fragment `Foo {{bar {baz}}}	quux` would break
down as:

* Plain text '`Foo `'
* A template instruction `bar {baz}`
* Plain text '` quux`'

Note that it is the "_not immediately followed by any more `}` characters_"
rule is what makes sure that the final `}` inside the template instruction,
closing the inner `{baz}`, is part of the template instruction, not part of
the closing `}}`.

**Work in progress; To be continued**
