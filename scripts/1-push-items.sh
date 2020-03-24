curl --request POST 'localhost:5005/api/items' \
	--header 'Content-Type: application/json' \
	--data-raw '{
		"ArticleNumber":"Pickle Rick",
		"Price": 27
	}'

	
curl --request POST 'localhost:5005/api/items' \
        --header 'Content-Type: application/json' \
        --data-raw '{
                "ArticleNumber":"Ananans",
                "Price":18
        }'

curl --request POST 'localhost:5005/api/items' \
        --header 'Content-Type: application/json' \
        --data-raw '{
                "ArticleNumber":"Milk",
                "Price":2
        }'

curl --request POST 'localhost:5005/api/items' \
        --header 'Content-Type: application/json' \
        --data-raw '{
                "ArticleNumber":"Milk",
                "Price":3
        }'

curl --request POST 'localhost:5005/api/items' \
        --header 'Content-Type: application/json' \
        --data-raw '{
                "ArticleNumber":"Pickle Rick",
                "Price": 99.99
        }'
