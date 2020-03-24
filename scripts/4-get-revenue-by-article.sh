url=localhost:5005/api/items/revenue-by-article

if [ "$1" ]; then
	url=$url'?day='$1
fi
curl --request GET $url
