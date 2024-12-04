## For testing using Redis CLI inside docker to connect to Redis server running on host machine

### **note that port is 6371**

```bash
docker run -it --rm --name redis-stack-cli -p 6379:6379 -p 8001:8001 redis/redis-stack:latest redis-cli -h host.docker.internal -p 6371
```

### Or use JavaScript Client Client

```bash
npm install -g redis-cli
rdcli -h host.docker.internal -p 6371
```
