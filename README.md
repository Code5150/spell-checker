# spell-checker
The spell checker corrects words by finding words in the
dictionary that are no more than two edits away from the input.

Here, an edit is either:
- Inserting a single letter, or
- Deleting a single letter

with the restriction that
- If the edits are both insertions or both deletions, they may not be of adjacent
characters.

As output, you should print the text lines with whitespace intact, with the following changes on
each word, W:
- If W is in the dictionary, print it as is.
- Otherwise, if W is not in the dictionary,
    - If no corrections can be found, print “{W?}”.
    - Ignore any corrections that require two edits if there is at least one that
requires only one edit; then . . .
    - If exactly one correction is left, print that word.
    - If more than one possible correction is left, print the set of corrections as “{W1
W2 · · ·}”, in the order they appear in the dictionary.

__Example:__

Input:

```
rain spain plain plaint pain main mainly
the in on fall falls his was
===
hte rame in pain fells
mainy oon teh lain
was hints pliant
===
```
Output:

```
the {rame?} in pain falls
{main mainly} on the plain
was {hints?} plaint
```
