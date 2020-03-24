url=localhost:5005/api/items/number

if [ "$1" ]; then
	url=$url'?date='$1
fi
curl --request GET ${url}

echo $url
