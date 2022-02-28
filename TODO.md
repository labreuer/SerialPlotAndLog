# Todo

* Look at [Reading line-by-line from a serial port (or other byte-oriented stream)](https://www.sparxeng.com/blog/software/reading-lines-serial-port) for cleaner, encapsulated processing of incoming data.
* Investigate [Getting Serial Port Information](https://stackoverflow.com/questions/2837985/getting-serial-port-information)
	* [`Win32_SerialPort`](https://github.com/labreuer/SerialPlotAndLog/blob/68a49d26b9224551776d159c5017f6dbddf5366e/SerialPortInfo.cs#L51) works for LB but not JJ
		* `Get-WMIObject -Class Win32_SerialPort` can be run in PowerShell
	* [`SerialPort.GetPortNames()`](https://github.com/labreuer/SerialPlotAndLog/blob/68a49d26b9224551776d159c5017f6dbddf5366e/SerialPortInfo.cs#L62) is buggy on hot unplug and replug for LB but not JJ
		* For LB, it was as if `SerialPort.GetPortNames()` merely accessed [`Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM"))`](https://github.com/labreuer/SerialPlotAndLog/blob/68a49d26b9224551776d159c5017f6dbddf5366e/SerialPortInfo.cs#L70)