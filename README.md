# 工具合集

## 介绍
为了简化一些代码封装了适合小项目使用的工具包

### http工具
1. HttpClientHelper

```
//先注入工具 工具使用的是单例生命周期
service.AddTokenHttpHelperInject("http://localhost:5104/");
...

//使用工具前先注入工具
private readonly TokenHttp _tokenHttp;
	public DemoService(TokenHttp tokenHttp){
		_tokenHttp=tokenHttp;
	}
...

//使用工具
var result= _tokenHttp.GetAsync("/api/demo");


```

### 自动注入工具
2.Inject

```
// 你需要注入的项目的程序集，一个项目只需要一个即可
services.AddAutoInject(typeof(DemoModule));

...

//使用工具  继承ITransientTag 将使用Transient生命周期 
// IScopedTag ITransientTag ISingletonTag 三种生命周期标志，继承不同接口生命周期也不同
public class DemoService:ITransientTag
{
}


```
