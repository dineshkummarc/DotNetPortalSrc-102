<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.Drawing.Drawing2d" %>
<%
' useage = <img src="counter/counter.aspx?src=digits.gif&digits=5&id=countername">


' ***** MANAGE COUNTER VALUE CREATION, RESTORATION, AND SAVING *****

' Get the counter ID (unique to each counter in the site)
dim counterid as String = request("id")

' Get current counter value
dim value as Integer = cint(Application("Counter_" & counterid))

if Session("CounterTemp_" & counterid) is Nothing then
	
	' If the text file exists load the saved value 
	' Always loading it from the file rather than just using the application variable allows for manual modification of counter values while the application is running (by editing the text file). To stop it from being able to be modified just uncomment the following 'if..then' and the 'end if'.  This will give a slight performance boost to the counter incrementing as it will stop a file operation.  Yes, I'm an optimization freak! :)
	'if value = 0 then
		if File.Exists(server.mappath(counterid & ".txt")) then
			dim sr as StreamReader = File.OpenText(server.mappath(counterid & ".txt"))
			value = cint(sr.ReadLine().ToString())
			sr.Close
		end if
	'end if
	
	' Increment counter
	value += 1
	
	' Save counter to an application var (the locks are there to make sure noone else changes it at the same time)
	Application.Lock()
   	Application("Counter_" & counterid) = value.ToString()
	Application.UnLock()
	
	' Save counter to a text file
	dim fs as FileStream = new FileStream(server.mappath(counterid & ".txt"), FileMode.Create, FileAccess.Write)
	dim sw as StreamWriter = new StreamWriter(fs)
	sw.WriteLine(Convert.ToString(value))
	sw.Close
	fs.Close
	
	' Set a session variable so this counter doesn't fire again in the current session
	Session.Add(("CounterTemp_" & counterid), "True")
end if

' ***** CREATE OUTPUT GRAPHIC FOR THE COUNTER *****

' Load digits graphic (must be in 0 through 9 format in graphic w/ all digits of set width)
dim i as System.Drawing.Image = System.Drawing.Image.FromFile(server.mappath(request("src")))

' Get digit dynamics from the graphic
dim dgwidth as Integer = i.width/10
dim dgheight as Integer = i.height

' Get number of digits to display in the output graphic
dim digits as Integer = cint(request("digits"))

' Create output object
dim imgOutput as New Bitmap(dgwidth*digits, dgheight, pixelformat.format24bpprgb)
dim g as graphics = graphics.fromimage(imgOutput)

dim j as Integer, dg as Integer
for j = 0 to (digits-1)
	' Extract digit from value
	dg = fix(value / (10^(digits - j - 1))) - fix(value / (10^(digits - j)))*10
	' Add digit to the output graphic
	g.drawimage(i, New rectangle(j*dgwidth, 0, dgwidth, dgheight), New rectangle(dg*dgwidth, 0, dgwidth, dgheight), GraphicsUnit.Pixel)
	
next j

' Set the content type and return output image
response.contenttype="image/jpeg"
imgOutput.save(response.outputstream, imageformat.jpeg)

' Clean up
g.dispose()
imgOutput.dispose()
i.dispose()
%>
