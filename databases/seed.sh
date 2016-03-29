# seed initial data to mongo database

mongoimport \
    --host 192.168.99.100 \
    --db bank \
    --collection bank_data \
    --file ./databases/bank_data.json