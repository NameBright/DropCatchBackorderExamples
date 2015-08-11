#!/bin/bash

# bash/curl examples for interacting with the DropCatch's API

#get a token
token=`curl -d grant_type=client_credentials -d client_id=<account name>:<application name> --data-urlencode "client_secret=<enter api secret here>" https://api.namebright.com/auth/token | sed  's/{"access_token":"\([^"]*\).*/\1/'`


#Standard Backorders
curl -X POST -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d '{"domains": ["testdomain1.com","testdomain2.com"]', "routingCode": "standard"}'  https://www.dropcatch.com/api/BackorderDomainsApi/BackOrderDomains

#Discount Club Backorders
curl -X POST -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d '{"domains": ["testdomain1.com,15","testdomain2.com,20"]', "routingCode": "discount"}'  https://www.dropcatch.com/api/BackorderDomainsApi/BackOrderDomains


#Cancel backorders (Standard or Discount)
curl -X POST -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d '["testdomain1.com","testdomain2.com"]'  https://www.dropcatch.com/api/BackorderDomainsApi/CancelBackOrders

