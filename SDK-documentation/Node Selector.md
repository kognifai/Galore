Node Selector
=============

The node selector is an expression for selecting one or more nodes from
the Galore. A node can be selected in several ways:

-   Path
-   Id
-   Attribute name
-   Attribute value
-   Template reference (planned obsolete)

Node selectors can also be "chained" to select nodes in multiple steps,
as described below. Nodes selected by the previous part of a chain
becomes the context in which the current part of a chain is applied.
This makes it possible to use relative paths.

path
----

A path selector is a path prefixed by a tilde `~` character. Paths are
similar to file system paths. Wildcards "*" will match any character in
a node tag. "." matches the current context node. ".." matches the
parent node. The context node depends on the execution context of the
selector, normally this is the root node or the previous part of a
chained selector. If the selector is part of a TQL query configured on a
calculator or a monitor node, that node is the context node. Examples:

- `/` Select the root node
- `/Farm/Turbine1`: Select the "Turbine1" node under the "Farm" node
-   `/Farm/Turbine*` Selects all nodes with tag starting with
    "Turbine" under the "Farm" node
-   `../Turbine1` Selects the sibling "Turbine1" of the current context node

id
--

An id selector is a numeric id prefixed by the hash/pound character
"#". The id selector is normally used in dynamic commands/or queries
where the id has been resolved and not in configured/static queries.
Selectors with ids cannot be reused in other instances of Galore
because the "same" node will have a different id.

attribute name
--------------

Attribute selectors appear in between square brackets "[" and "]"

Example: 
```
[maxPower]
```
Select all nodes with the attribute "maxPower"

attribute value
---------------

Attribute selectors appear in between square brackets "[" and "]"

Example: 
```
[maxPower=2300]
```
Select all nodes with the attribute "maxPower" of value 2300

Chained selectors
-----------------

Multiple selectors can be chained to select nodes in a context. Chained
selectors are separated with a space character.

Examples:
```
/Farm1 [maxPower=2300]
```
Select all nodes under "Farm1" with the attribute "maxPower" of value 2300

And selectors
-------------

Multiple criteria can be specified for a node to match for selection.
Multiple criteria are not separated with any characters.

Example:
```
/Farm1 [nodeType=Turbine][maxPower=2300]
```
Select all nodes under "Farm1" with the attribute "nodeType" of value "Turbine" AND the attribute "maxPower" of value 2300.

Or Selectors
------------

Multiple criteria can be specified for nodes to be included in a
selection. Multiple criteria are separated with comma "," characters.

Example:
```
/Farm1 [nodeType=Turbine], /Farm1 [maxPower=2300]`
```
Select all nodes under "Farm1" with the attribute "nodeType" of value "Turbine" OR the attribute "maxPower" of value 2300.

Note: There is presently no way to group parts of the selector, e.g. to
write the above example as

Not allowed: `/Farm1 ([nodeType=Turbine], [maxPower=2300])`

Parameters
==========

Some parameters have special syntax and are used in many operations.

Interval
--------

Interval can be a floating point number with a time unit postfix "s",
"m", "h", "d", "w", "M", "y". Examples:

-   `25s`: 25 seconds

-   `2m`: 2 minutes

-   `1.5h`: 1 hour 30 minutes

The interval can also be written as mm:ss where mm is the number of
minutes and ss is the number of seconds.

Period
------

A period is similar to an interval and can have all the same values. In
addition a period can have the values "week", "month", "year",
"quarter", "decade" and "century".

The main difference between a period and an interval is that periods are
aligned to the local time. An interval of 24h is just an amount of time.
A period of 24h is aligned to the local time. A period parameter is
usually used to "periodize" a sequence into a new sequence of events
grouped by the period they occurred in.

Expression
----------

Some operations take one or more expressions as their parameter.

Expression are have the same syntax as C# expressions and are between
single quotes: `'expression'`

The variable "v" refers to the input vector of the expression if the
input is a time series type. v[0] refers to the first element of the
vector, v[1] to the second and so forth. For alarm input the variable
b is used. m

Expressions are optionally followed by a result unit identifier. The
system is not yet capable of inferring the result unit from the
expression. If the unit is omitted, the result is assumed to have the
same unit as the first input.

Examples:

-   'v[0] + v[1]'

-   'v[0] * v[1]' Power:SI:MW

-   'v[0] > 5' (bool output)

-   'v.Sum()'

-   'sin(v[0])'

-   'b[0] && b[1]' (bool input and output)

Time
----

Format is close to ISO8601

Examples:

-   `2015-03-15`: 15<sup>th</sup> of mars 2015

-   `2015-03-01T01:13`: 13 past one, 1<sup>st</sup> of mars 2015
-   `2015-03-15T12:47:33.12345Z 12:47:33`: 15<sup>th</sup> of mars 2015

Note the "T" separating date and time

Note the Z designating the time zone "Zulu" = UTC/GMT
