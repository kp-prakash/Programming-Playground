var results = [];

function calculateFibonacciValue(n) {
	var sum = 0;
	var returnValue;
	if(n == 0) {
		return sum;
	}
	if(n == 1) {
		sum += 1;
		return sum;
	} else {
		return (calculateFibonacciValue(n - 1) + calculateFibonacciValue(n - 2));
	}
};

function generateFibonacciSeries(n) {
	for (var i = 0; i <= n-1; i++) {
		results.push(calculateFibonacciValue(i));
	}
	postMessage(results);
};

function messageHandler(e) {
    if(e.data > 0){
        generateFibonacciSeries(e.data);
    }
};

addEventListener("message", messageHandler, true);