//Writing a hello world unit test with QUnit.
test("Hello World!", function () {
    ok(true);
});

/*Modules are equivalent to Fixtures in frameworks like NUnit and JUnit.
Define a module when your test code needs some common setup and teardown.

Purposes of Modules
    Group Tests Logically
    Group Setup & Teardown
*/

module('Module 1')

//Writing a hello world unit test with QUnit.
test("First Test!", function () {
    ok(true);
});

//Observe the result in the test runner.
//It will be shown as "Module 1: First Test! (0, 1, 1)"
//The test belongs to the module and it is prefixed with module name.

//Let us define one more module and a test

module('Module 2')

//Writing a hello world unit test with QUnit.
test("Second Test!", function () {
    ok(true);
});

//Now the output will be "Module 2: Second Test! (0, 1, 1)"
//Note that tests are not grouped inside module, they just follow the module.
//If we declare a test after a module, it belongs to the module.


/*Setup and Teardowm
Inoder to have a setup that is run before all the tests and a teardown that is run
after all the tests, implement them as shown below.
Module takes setup and teardown functions.

Purposes of Common Setup
    Common Objects
    Setting up the DOM

Purposes of Common Teardown
    Cleaning up the DOM
    Generic cleanup

Reasons for Using Multiple Modules
    Logical grouping of tests
    Grouping by component
    Grouping by common setup / initial state
*/
module('Module 3', {
    setup : function () {

    },
    teardown: function () {

    }
});

/*The test files and source files can be organized in any fashion.
1 test file for 1 source file.
1 test file for many source files.
Many test files for one source file.
Many test files for many sources files.
This purely depends on the scenario under test.

TestRunner1.Html
    TestFile1.js
    SourceFile2.js
    SourceFile1.js
TestRunner2.Html
    TestFile2.js
    TestFile3.js
    SourceFile3.js
TestRunner3.Html
    TestFile4.js
    TestFile5.js
    SourceFile4.js
    SourceFile5.js
*/


/*Running Tests
01. Running all tests within a test runner file.
02. Running a single test in a test runner file. - Click on the 'Rerun' button to run that test.
03. Custom Filter. - Observe the URL upon clicking the 'Rerun'.
    It changes to '...ExploringQUnit/Getting%20Started/Index.html?testNumber=1'. Note testNumber attribute.
04. Using the composite addon.
    Say we have tests across different test runner files. The composite addon helps in invoking multiple test runners.

        Steps to use QUnit Composite Addon:
        1. Add references to QUnit and QUnit Composite.
            <link rel="stylesheet" href="qunit.css" type="text/css" media="screen">
            <link rel="stylesheet" href="qunit-composite.css">
            <script src="qunit.js"></script>
            <script src="qunit-composite.js"></script>
        2. Invoke QUnit.testSuites() with the paths of test runner html files.
            <script>
            QUnit.testSuites([
                "test-1.html",
                "test-2.html",
                "test-3.html"
            ]);
            </script>
        3. Easy huh?
05. ReSharper - Visual Studio
    ReSharper adds little marker on the left pane which allows to run the tests.
    On top of the test files just add the code shown below to refer to source code being tested.
    
    ///< reference path = "MyClass.js" />
*/

/*
Integrating QUnit with the DOM
    * Reasons to test the DOM
        ~ Test that the System Under Test correctly manipulates the DOM.
        ~ Test that the System Under Test correctly reads from the DOM.
    * Drawbacks to DOM testing
        ~ Requires additional setup
        ~ Prone to brittleness
*/

module('Todo Test Module.', {
    setup: function () {
    },

    teardown: function (){        
    }
});

test('Test Create Todo Item.', function () {
    SUT.createTodoItem();
    //Always use a class to test DOM elements, instead of using element name.
    //Sometimes elements might change and make the tests brittle.
    strictEqual($(".js-todo-item").length, 1);
});

/*
Integrating QUnit with CI

Basics: Use PhantomJS and capture output
    * True Browser Testing Needs
        ~ Difficult because of deployment (Multiple versions of I.E.)
        ~ Typically easier to use a 3rd party.

    *URLs
        ~ PhantomJS - http://code.google.com/p/phantomjs/downloads/list
        ~ Qunit.teamcity.js - https://gist.github.com/1755675
*/

/*
Asynchronous Tests
    * Testing with setTimeout and setInterval
    * Testing with UI effects
    * Testing with ajax
        ~ Avoid it at all costs
        ~ Write an abstraction layer
        ~ Use test doubles
*/

module('Asynchronous Tests');

test('Broken asynchronous test!', function () {
    setTimeout(function () {
        ok(true); //Assert inside the timeout.
        //Since the assert is inside the setTimeout(), we get error shown below.
        //'Expected at least one assertion, but none were run - call expect(0) to accept zero assertions.'
    }, 100);
    /* 
    If we have the Assert outside the setTimeout, ok() 
    will be called before code under test gets executed.
    */
});

//This is where asynchronous capabilities of QUnit helps us.
test('Asynchronous test!', function () {
    stop(); //Instructs QUnit to stop running tests.
    setTimeout(function () {
        ok(true); 
        start(); //Instructs QUnit to resume running tests.
    }, 100);
});

//Scenario with multiple setTimeout() calls.
test('Multiple setTimeout calls!', function () {
    stop(); //Instructs QUnit to stop running tests.
    stop(); //Have two calls to stop() so that QUnit knows it needs two calls to start().
    /*Instead of calling stop() twice, we can pass in number of calls as parameter - stop(2)*/
    setTimeout(function () {
        ok(true); 
        console.log('Longer setTimeout call completed!');
        start(); //Instructs QUnit to resume running tests.
    }, 1000);

    setTimeout(function () {
        ok(true); 
        console.log('Shorter setTimeout call completed!');
        start(); //Instructs QUnit to resume running tests.
    }, 100);
});

//asyncTest() method is a much elegant way of calling asynchronous tests
//No need to call stop() as it is implied.
asyncTest('Asynchronous test using asyncTest()!', function () {
    setTimeout(function () {
        ok(true); 
        start(); //Instructs QUnit to resume running tests.
    }, 100);
});


//UI test using asyncTest
asyncTest('UI test using asyncTest - for FadeOut!', function () {
    SUT.fadeOut(200);
    setTimeout(function () {
        ok(!$("#container").is(":visible")); 
        start(); //Instructs QUnit to resume running tests.
    }, 500);
});

//Better way to perform UI testing.
asyncTest('UI test using asyncTest - using a callback function.', function () {
    SUT.fadeOutWithCallBack(200, function () {
        ok(!$("#container").is(":visible")); 
        start(); //Instructs QUnit to resume running tests.
    });
});

/*
QUnit Tidbits
    * Noglobals setting
    * Notrycatch setting
    * Expect() method
    * Events
        ~ log - raised every time as assert is passed
        ~ testStart - raised whenever a test is started
        ~ testDone - raised whenever a test is completed
        ~ moduleStart - raised whenever a module is started
        ~ moduleDone - raised whenever a module is completed
        ~ begin - raised at the beginning when all the tests are started
        ~ done - raised at the end when all the tests are completed
*/

/*Noglobals setting*/
module('QUnit Tidbits!');

test('Noglobals setting.', function () {
    globalVar = 2; //This will cause failure when run with 'Check for globals' setting turned on.
    //Fix: Declare local variable.
    var localVar = 3;
    strictEqual(2, globalVar);
    strictEqual(3, localVar);
});

test('Hidden exception!', function () {
    //Uncomment the line below and run QUnit with 'No try-catch' setting turned on.
    //SUT.doSomething(); 
    ok(true);
});

test('Expect() method!', function() {
    expect(2);
    ok(true);
    ok(true);
    //expect() instructs QUnit on the number of asserts expected.
    //If there is a mismatch, the following error is thrown.
    //"Expected x assertions, but y were run."
});

test('Expect() method - alternative!', 2,  function() {
    //Second parameter is the number of asserts expected.
    ok(true);
    ok(true);
    //expect() instructs QUnit on the number of asserts expected.
    //If there is a mismatch, the following error is thrown.
    //"Expected x assertions, but y were run."
});

//QUnit Events
// log - raised every time as assert is passed
QUnit.log = function(args) {
    console.log("Passed an assert: " + args.result);
};

// testStart - raised whenever a test is started
QUnit.testStart = function(args) {
    console.log('Started test ' + args.name);
}

// testDone - raised whenever a test is completed
QUnit.testDone = function(args) {
    console.log('Finished test ' + args.name);
}

// moduleStart - raised whenever a module is started
QUnit.moduleStart = function(args) {
    console.log('Started module ' + args.name);
}


// moduleDone - raised whenever a module is completed
QUnit.moduleDone = function(args) {
    console.log('Module done ' + args.name);
}

// begin - raised at the beginning when all the tests are started
QUnit.begin = function() {
    console.log('Started tests');
}

// done - raised at the end when all the tests are completed
QUnit.done = function(args) {
    console.log('Finished all tests.  tests passed: ' + args.passed + "/" + args.total);
}
