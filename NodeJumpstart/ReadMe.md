NodeJumpStart is mainly aimed at Node.js beginners, having said that knowledge of JavaScript is a must.

#Few core modules in Node.js

* `net`: For creating TCP clients and servers
* `http`: For creating and consuming HTTP services
* `fs`: For accessing and manipulating files
* `dns`: For using the DNS service
* `events`: For creating event emitters
* `stream`: For creating streams
* `os`: For accessing some local operating system statistics
* `assert`: For assertion testing
* `util`: For miscellaneous utilities

#Top 5 features of Node

##Modules in Node.js

Node.js has three types of modules:
* Core Modules
* User Modules
* 3rd Party Modules

### The `require` function
The require function is used to import a module to enable using it in other modules. In a way it works

* The path mentioned is relative to current file. e.g. ./, ../, etc.
* Multiple modules could be imported in this fashion and assigned to variables.
* The trailing .js extension is optional.

## Node Package Manager
Node.js has a way of browsing, querying, installing, and publishing third-party modules into a central repository, and it's called NPM. NPM stands for Node Package Manager, and it consists of two things:
* A module repository that is fully browsable, accessible at https://npmjs.org/
* A command-line utility

In NPM, each packaged module has a name and a version. A module can have more than one version and we can pick the version that we like.

Let us create a package.json file and try to observe that in action. It defines the name, version and dependencies for an application. Let us place this file in the root of the application folder and see what happens when we run the `npm install` command.

```js
{
    "name": "my-app",
    "version": "0.1.0",
    "dependencies": {
        "request": "*",
        "nano": "3.3.x",
        "async": "~0.2"
    }
}
```

Open the application folder and run `npm install` command. You will see a list of `http` calls and NPM downloads all required dependencies into a folder named 'node_modules'.

This last command not only lists the NPM modules installed locally but also the modules that those modules depend on, recursively rendering it into this tree.

```
async@0.2.10 node_modules\async

request@2.34.0 node_modules\request
├── json-stringify-safe@5.0.0
├── forever-agent@0.5.2
├── qs@0.6.6
├── aws-sign2@0.5.0
├── tunnel-agent@0.3.0
├── oauth-sign@0.3.0
├── mime@1.2.11
├── node-uuid@1.4.1
├── tough-cookie@0.12.1 (punycode@1.2.4)
├── hawk@1.0.0 (cryptiles@0.2.2, sntp@0.2.4, boom@0.4.2, hoek@0.9.1)
├── http-signature@0.10.0 (assert-plus@0.1.2, asn1@0.1.11, ctype@0.5.2)
└── form-data@0.1.2 (combined-stream@0.0.4)

nano@3.3.8 node_modules\nano
├── errs@0.2.4
├── underscore@1.4.4
├── follow@0.8.0 (request@2.2.9)
└── request@2.16.6 (forever-agent@0.2.0, aws-sign@0.2.0, tunnel-agent@0.2.0, oauth-sign@0.2.0, json-stringify-safe@3.0.0, cookie-jar@0.2.0, node-uuid@1.4.1, mime@1.2.11, qs@0.5.6, hawk@0.10.2, form-data@0.0.10)
```

We mentioned version number for each of the dependency in 'package.json' file. This version numbering is based on a scheme known as semantic version. The format is Major.Minor.Revision.

* Asterik (*) is used as a wildcard and stands for any version.
* 3.3.x means any patch version of 3.3 will suffice.
* ~0.2 has similar effect as previous item and means any patch version of 0.2

NPM will analyze versions in package.json and will install the latest version that matches the specification, if it is available.

Once installed these could be used in Node.js apps as shown below. This is where require.js is used to import third party modules. Node.js will look for the modules inside the 'node_modules' directory.

```js
var request = require('request');
var nano = require('nano');
var async = require('async');
```

###Dependency Resolution - Node.js Modules
Node.js's dependency resolution is pretty interesting. If a module is not found in 'node_modules' directory, Node.js keeps looking for it in the parent directory and climbs up the file system tree until it finds the requested module or reaches the root directory. If there is some code in a nested directory, it can refer to the code present in a module installed at the root of the application folder.

Let us assume that we create a library for a database module in 'Library/database.js', this can depend on the 'async' module as shown below. Node.js does the hard work of locating this dependency.
```js
var async = require('async');
```

##Working with Streams in Node.js
Streams could be considered as data distributed over time. An application can process data as it comes by bringing data in chunks rather than waiting for all of the data to arrive and then process it.

The below example streams an image from server to client. The HTTP module is used to create and start a server. The File System module is used to create a read stream and the output is streamed using the response object. Please refer to https://github.com/substack/stream-handbook to understand streams.

```js
var http = require('http');
var fs = require('fs');
http.createServer(function (request, response) {
    response.writeHead(200, {'Content-Type': 'image/png'});
    //This is a file which is 5 MB in size.
    fs.createReadStream('./image.png').pipe(response);
}).listen(3000);

console.log('Server up and running @ http://localhost:3000');
```

##WORK IN PROGRESS