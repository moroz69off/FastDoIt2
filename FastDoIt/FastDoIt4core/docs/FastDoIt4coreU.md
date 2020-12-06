# FastDoIt!
______________________
### Application requires
This web application requires some dependencies:

*for windows -*
- Selenium web driver (driver for chrome, dounload automatic)
- Web brouser Chrome 
- .NET Core 3.1 [Dounload](https://dotnet.microsoft.com/download/dotnet-core/3.1)

*for linux -*
- Selenium web driver (driver for chrome, dounload automatic)
- Web brouser Chrome
- .NET CORE 3.1

__________________________________
### Profiles
The profile information line contains data according to the template: *size, Name, LastName, e-mail, address, sity, postindex, phone, cardNum, maintenance (00/0000), Security code.*

**example:** "***42, Ivan, Ivanov, ivanivanov@mail.to, Shabolovka 27, Moscow, 127000, +7(495)1234567, 1234432100009999, 12/2021, 123***"
#### Notes profiles
```diff
In the "profiles.csv" file 
you cannot edit the first two lines:
a line with headers 
and a line with a profile for debugging.
Write your profiles from 3 lines.
```

___________________________________

### Link`s
In the root directory of the application, in the "links" file (without the name extension), you need to write the links for processing, each on a new line.

___________________________________
### CMD args
- "-D" Debug mode. If this flag, then the remaining arguments cannot be added
- "-T 0000" Timeout, after a space specify the timeout time in milliseconds
- "-I 000" Interval. Determines how often the application requests a web page with a product unit
- "-P 0" Number of profile. Assigns a profile number for the application to run. The list of profiles should be recorded in the "profiles.csv" file.

**example:** "***-T 3600 -I 500 -P 2***" or "***-D***"

