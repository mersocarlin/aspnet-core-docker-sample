# seed initial data to mongo database

mongoimport \
    --host 192.168.99.100 \
    --jsonArray \
    --db bank \
    --collection bank_data < ./databases/bank_data.json