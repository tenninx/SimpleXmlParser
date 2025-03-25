# SimpleXmlValidator ReadMe

## SimpleXmlValidator is a console application that validates a given XML statement in its simpliest form e.g. no attributes.

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

Note that the input XML should not contain any attributes in any XML nodes. That can be implemented when I have the clearance to do that.

Though unusual, XML permits the following data, which has been implemented in my logic to allow that:
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