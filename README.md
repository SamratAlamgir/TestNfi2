**Update this README.md file**.

 A README.md file is intended to quickly orient readers to what your project can do.
 [Learn more](http://go.microsoft.com/fwlink/p/?LinkId=524306) about Markdown.
 
 Short background Of this project 
 **********************************************************************************************
 
 I had a meeting with the Norwegian Film Institute (NFI) yesterday.
 
They need a fast delivered emergency solution.
 
Due to political decisions, they need to complete and make 7 new application forms available online by January 1st. 2016
The forms is to be created as HTML forms and delivered from us as Iframes. One iframe pr. Application form.
When submitting the form the following thins should happen:
* Application is sent by mail to a given mail address.
* Receipt sent by mail to the person submitting the application.
* The application is stored on the server (In case something fails, they can check with the "database")
 
No complex error-handling.
In the most complex application form there are 20 files that needs to be uploaded plus a lot of textfields, yes-no choices etc.
We should have a max limit of 100MB per. file
No complex fields nor much "javascript magic".
Many fields are mandatory.
 
Cefalo is required to host the solution (Amazon or Azure?)
 
Simple admin interface:  
A simple admin interface that displays incoming applications per. form sorted by date.Showing a list of 25 pr. page.
Should be able to “download” individual applications from this interface
Should maybe not be able to erase (easily at least), but maybe archive, so they don’t show up in the list?
Must have basic authentication.
 
Security?
We have not discussed that yet, but we don’t want spammers to fill our servers with crap applications and files.
 
The solution will live for one year until å more proper solution is in place. (But if we make this much more proper than they aks for it might lead to more work and a longer living solution ??? J )
 
I have attached an example application. The most complex one. The other 6 application forms are supposed to be simpler than this.