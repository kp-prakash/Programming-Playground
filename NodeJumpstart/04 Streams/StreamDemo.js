var http = require('http');
var fs = require('fs');
http.createServer(function(request, response) {
  response.writeHead(200, {
    'Content-Type': 'image/png'
  });
  //This is a file which is 5 MB in size.
  fs.createReadStream('./image.png').pipe(response);
}).listen(3000);

console.log('Server up and running @ http://localhost:3000');
