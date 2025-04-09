# SimpleXmlValidator ReadMe

## SimpleXmlValidator is a console application that validates a given XML statement.

### The console executable "Main.exe" accepts 2 params as follows:
```
Main.exe "{xml_string}" {algorithm}
```
`{xml_string}` is the given XML to validate. Must be between double quotes.<br/>
`{algorithm}` is the algorithm to use. `1` for recursive or by default any others for linear (looping).

### For example, to validate `<Design><Code>hello world</Code></Design>` using recursive method:
```
Main.exe "<Design><Code>hello world</Code></Design>" 1
```

#### The application implements a simpler version of stack called `SimpleStack` instead of depending on the .NET `Stack` class.

The code has been updated to support attributes in any XML node. Here is an example:
```
<Design>
	<Line number="1">
		<Code>good morning world</Code>
		<People>good person</People>
	</Line>
	<Line number="2">
		<Code>good night world</Code>
		<People>nice people</People>
	</Line>
</Design>
```

Though unusual, XML permits data in between different tags, which has been implemented in my logic to allow that:
```
<Design>
	<Line>some data
		<Code>good morning world</Code>
		<People>good person</People>
	</Line>
	<Line>
		<Code>good night world</Code>
		<People>nice people</People>more data
	</Line>
</Design>
```