# B2-RISK

Pour chaque nouvelle fonctionnalité tirée une nouvelle branche depuis celle-ci puis nommée là avec la fonctionnalité que vous voulez ajouter

Vous devez installer la dependance avant de lancer le projet.

## Specification

- .NET 6
- Prérequis avant lancement:

![image](https://user-images.githubusercontent.com/91829231/193802735-e29d8524-2eb2-48b8-ac12-8679abedfb07.png)
   
- Lancez la solution puis allez dans les propriétés de la solution et sélectionnée compilation multiple et choissez démarrer comme action pour RISKAPI et JurrasicRisk. 

# Redis Setup
## Prérequise (REDISJSON , REDISEARCH , REDIS-SERVER)
```
sudo apt install lsb-release
```
## Add apt redis
```
curl -fsSL https://packages.redis.io/gpg | sudo gpg --dearmor -o /usr/share/keyrings/redis-archive-keyring.gpg

echo "deb [signed-by=/usr/share/keyrings/redis-archive-keyring.gpg] https://packages.redis.io/deb $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/redis.list

sudo apt-get update
sudo apt-get install redis
```
# Add modules (REDISJSON)
```
git clone --recursive https://github.com/RedisJSON/RedisJSON.git
cd RedisJSON
```
# Install dependencies
```
./sbin/setup
```
# Build
```
Make build
```
# Move modules to /etc/redis/modules need to be sudo
```
cd src/

mkdir /etc/redis/modules/

mv librejson.so /etc/redis/modules/

./deps/readies/bin/getredis
```

# Add modules (REDISEARCH)
```
git clone --recursive https://github.com/RediSearch/RediSearch.git
cd RediSearch

```
# Install dependencies
```
./sbin/setup
```
# Build
```
Make build
```
# Move modules to /etc/redis/modules need to be sudo
```
cd bin/linux-x64-release/search/

mkdir /etc/redis/modules/

mv redisearch.so /etc/redis/modules/
```

## Config
Add 'LOADMODULE' to Redis.conf /etc/redis/redis.conf
# Start Redis
```
redis-server --loadmodule /etc/redis/modules/redisearch.so --loadmodule /etc/redis/modules/librejson.so
or
redis-server /etc/redis/redis.conf
```
# Check if modules are loaded
```
redis-cli info modules
```
# excepted result 
![image](https://user-images.githubusercontent.com/91829231/209672595-00118994-b348-4a04-a6ad-ad3aa1ce8d3f.png)

