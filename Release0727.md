Release 0727
==========================

## 程序包：

- Notes.zip (见附件)

## 配置：

- 接口配置：api.Production.json

```json
"UserHostBase": "http://test.api.forclass.net/",
```
请把以上地址更新为正式环境地地

- 系统配置

请修改以下配置：

- 读写数据库链接字附串

- Redis链接设置

- ```IsSubscriber``` 是否消息队列的订阅者，建议开户总服务器的1/2，如10台IIS中至少有5台开户.



```json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Error",
      "Microsoft.Hosting.Lifetime": "Error"
    }
  },
  "ConnectionStrings": {
    // 只读数据库
    "NotesConn": "Server=49.233.204.143,3433;Database=notes;User Id=prophet;Password=wy2004start;",
    // 可写数据库
    "NotesWriteConn": "Server=49.233.204.143,3433;Database=notes;User Id=prophet;Password=wy2004start;"

  },
  "easycaching": {

    "inmemory": {
      "CachingProviderType": 1,
      "MaxRdSecond": 120,
      "Order": 2
    },
    "redis": {
      "CachingProviderType": 2,
      "MaxRdSecond": 0,
      "Order": 2,
      // Redis相关配置
      "dbconfig": {
        
        "Password": "ABCabc01",
        "IsSsl": false,
        "SslHost": null,
        "ConnectionTimeout": 5000,
        "AllowAdmin": true,
        "Endpoints": [
          {
            "Host": "127.0.0.1",
            "Port": 6379
          }
        ],
        "Database": 1
      }
    }
  },

  "DomainOptions": {
    "NotesCachePrefix": "271Notes_14_",
    "BodyCachePrefix": "271Notes_body_14_",
    "UserCachePrifex" : "271Notes_user_14_"
  },

  //消息队例订阅
  "IsSubscriber":true 

}


```


## 消息队列设置 CAPSettings.json

修改以上配置：

```json

{
  "ABPSettings": {
    "HostName": "49.233.75.253", //主机地址
    "Password": "ABCabc0325", // 密码
    "UserName": "rabbit_mq_user", // 用户名
    "Port": 5672 //端口号
  }

}
```

## 日志 （Nlog.config）

修改以下```filename```部分中的路径```D:\tools\grafanaloki\log```为正式环境的日志存放路径

```
  <target xsi:type="File" name="allfile"
            fileName="D:\tools\grafanaloki\log\${shortdate}-${uppercase:${level}}.log"
            layout="${longdate}|${event-properties:item=EventId_Id}|${uppercase:${level}}|${logger}|${message} ${exception:format=tostring}"
            archiveFileName="${aspnet-appbasepath}\log\${shortdate}-${uppercase:${level}}-{###}.log"
          archiveNumbering="Rolling"
          archiveAboveSize="1242880"
            archiveEvery="Day"/>

```


## 数据库脚本

- 创建 ```Notes```数据库(读、写请定义)

- 执行 ```Notes20200727.sql```脚本


