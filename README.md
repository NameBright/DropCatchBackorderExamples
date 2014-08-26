DropCatchBackorderExamples
==========================

We have exposed DropCatch.com API functions so that you can programmatically backorder domains. This feature makes use of NameBright.com API accounts, and is very similar to using the REST endpoint for the NameBright.com API.

Requirements
------------
1. A NameBright.com account. Create one here: https://www.namebright.com/NewAccount
2. The NameBright.com account must be activated for API access. Start this process here: https://www.namebright.com/Settings#Api
3. Your API account in NameBright.com must have the "Register Domains" permission.
4. Your API account must have an IP whitelist defined.
5. You must have logged into DropCatch.com using your NameBright.com credentials at least once.
6. Before calling the API functions, you must retrieve a bearer token from the NameBright auth endpoint (see the examples).

Example Code
------------
Use the following examples to get started with the DropCatch API:
- CSharpExample: This is .NET 4.5 code which uses HttpClient to POST json to the DropCatch API. 
- BashCurl: a bash shell script which uses curl to POST json to the DropCatch API

Download all example code from here: https://github.com/NameBright/DropCatchBackorderExamples/archive/master.zip

Supported Functions
-------------------
- Backorder one or more domain names
- Cancel one or more domain backorders

Example Responses
-----------------

- Success
```json
{
  "someErrors": false,
  "results": [
    {
      "domainName": "TestDomain1.com",
      "success": true,
      "maxBid": "59.00",
      "message": "Backorder Accepted",
      "statusCode": "200.1"
    },
    {
      "domainName": "TestDomain2.com",
      "success": true,
      "maxBid": "59.00",
      "message": "Backorder Accepted",
      "statusCode": "200.1"
    }
  ]
}
```

- Partial Failure
```json
{
  "someErrors": true,
  "results": [
    {
      "domainName": "Foo.com",
      "success": false,
      "maxBid": "0.00",
      "message": "Foo.com is not in pending delete status.",
      "statusCode": "411.0"
    },
    {
      "domainName": "TestDomain3.com",
      "success": true,
      "maxBid": "59.00",
      "message": "Backorder Accepted",
      "statusCode": "200.1"
    }
  ]
}
```


