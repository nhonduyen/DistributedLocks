Demo apply distribute locks using RedLock.net
Link: https://github.com/samcook/RedLock.net

- There are 2 service update the column 'Like' concurrently. 
- To make sure the like counting is correct, we use distributed lock on Redis to make sure only one service update the data at a time.