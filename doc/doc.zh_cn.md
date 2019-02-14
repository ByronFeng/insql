
# Insql ˵���ĵ�
## Ŀ¼
* **[MyBatis Sql Xml �﷨]()**
* **[�����ݿ�֧��]()**
* **[��̬�ű�֧��]()**
* **[��������������ʵ����־��¼]()**
* **[��ѯ�﷨]()**
* **[�����÷�]()**

### 1.MyBatis Sql Xml �﷨
Mybatis 3 sql xml ���Ƶ������﷨��Ŀǰ֧���������ýں�Ԫ�ء����Բ鿴 [Mybatis�ĵ�](http://www.mybatis.org/mybatis-3/zh/dynamic-sql.html)
- sections
    - **sql**
    `[id]`
    - **select** : _sql�ڵı���_
    - **insert** : _sql�ڵı���_
    - **update** : _sql�ڵı���_
    - **delete** : _sql�ڵı���_
 - elements
    - **include** `[refid(����sql���ý�)]`
    - **bind** `[name]` `[value(javascript �﷨)]`
    - **if** `[test(javascript �﷨)]`
    - **where** ��_��� `where` sql ��䲢���Ƴ���ͷ��and ����or_ 
    - **set** ��_��� `set` sql ��䵽update��. ����ɾ������ `,`_
    - **trim** `[prefix]` `[suffix]` `[prefixOverrides]` `[suffixOverrides]` _������Ӻ��Ƴ���ͷ�ͽ�β�Զ�����ַ�_

## 2.�����ݿ�֧��
�����ݿ�֧��ΪĬ�����ã�ʹ��ʱ�ǳ��򵥡�
### ʹ�÷�ʽ
_`xxx.insql.xml`�������ǰʹ�õ���SqlServer���ݿ⣬�������ʹ��`InsertUser.SqlServer`�����δ�ҵ���׺��`.SqlServer`�����ýڣ���ʹ��Ĭ�ϵ�`InsertUser`_
``` xml
<insert id="InsertUser">
insert into user_info (user_name,user_gender) values (@UserName,@UserGender);
select last_insert_rowid() from user_info;
</insert>

<insert id="InsertUser.SqlServer">
insert into user_info (user_name,user_gender) values (@UserName,@UserGender);
select SCOPE_IDENTITY();
</insert>
```
### ��ν��ö����ݿ�ƥ��֧��
```c#
public void ConfigureServices(IServiceCollection services)
{
	services.AddInsql(builder=> 
        {
            builder.AddDefaultResolveMatcher(options => 
            {
                options.CorssDbEnabled = false;  //Ĭ��Ϊ true
            });
        });
}
```
### ����޸�ƥ��ָ��
```c#
public void ConfigureServices(IServiceCollection services)
{
	services.AddInsql(builder=> 
        {
            builder.AddDefaultResolveMatcher(options => 
            {
                options.CorssDbSeparator = "@"; //Ĭ���� `.`
            });
        });
}
```
_`xxx.insql.xml`���޸�Ϊ `InsertUser@SqlServer`_
``` xml
<insert id="InsertUser">
insert into user_info (user_name,user_gender) values (@UserName,@UserGender);
select last_insert_rowid() from user_info;
</insert>

<insert id="InsertUser@SqlServer">
insert into user_info (user_name,user_gender) values (@UserName,@UserGender);
select SCOPE_IDENTITY();
</insert>
```
## 3.��̬�ű�֧��
### ������ת��
_`xxx.insql.xml`�� `test="userGender !=null and userGender == 'W' "` Ϊ��̬�ű�����Ϊ`&&` ��xml�����������壬����ʹ�� `and` ���滻 `&&`��������_
``` xml
<if test="userGender !=null and userGender == 'W' ">
 and user_gender = @userGender
</if>
```
_������ת��ӳ���_
`"and"->"&&"` `"or"->"||"` `"gt"->">"` `"gte"->">="` `"lt"->"<"` `"lte"->"<="` `"eq"->"=="` `"neq"->"!="`

### ���ò�����ת��
```c#
public void ConfigureServices(IServiceCollection services)
{
	services.AddInsql(builder=> 
        {
            builder.AddScriptCodeResolver(options => 
            {
                options.IsConvertOperator = false;  //Ĭ��Ϊ true
            });
        });
}
```
### ö��ת��Ϊ�ַ���
_`xxx.insql.xml`��`userGender == 'W'`,`userGender`Ϊö�����ͣ�����Ĭ��ת��Ϊ�ַ�������_
``` xml
<if test="userGender !=null and userGender == 'W' ">
 and user_gender = @userGender
</if>
```
### ����ö��ת��Ϊ�ַ���
```c#
public void ConfigureServices(IServiceCollection services)
{
	services.AddInsql(builder=> 
        {
            builder.AddScriptCodeResolver(options => 
            {
                options.IsConvertEnum = false; //Ĭ��Ϊ true
            });
        });
}
```
_`xxx.insql.xml`����Ҫ�޸�Ϊ`userGender == 1`_
``` xml
<if test="userGender !=null and userGender == 1 ">
 and user_gender = @userGender
</if>
```
## 4.��������������ʵ����־��¼
### ������ʹ�ù�����
_`OnResolving`Ϊ������ǰִ�У�`OnResoved` Ϊ��������ִ��_
```C#
public class LogResolveFilter : ISqlResolveFilter
    {
        private readonly ILogger<LogResolveFilter> logger;

        public LogResolveFilter(ILogger<LogResolveFilter> logger)
        {
            this.logger = logger;
        }

        public void OnResolved(InsqlDescriptor insqlDescriptor, ResolveContext resolveContext, ResolveResult resolveResult)
        {
            this.logger.LogInformation($"insql resolved id : {resolveContext.InsqlSection.Id} , sql : {resolveResult.Sql}");
        }

        public void OnResolving(InsqlDescriptor insqlDescriptor, string sqlId, IDictionary<string, object> sqlParam, IDictionary<string, string> envParam)
        {
        }
    }
```
_���ù�����_
```c#
public void ConfigureServices(IServiceCollection services)
{
	services.AddInsql(builder =>
        {
            builder.AddResolveFilter<LogResolveFilter>();
        });
}
```

## 5.��ѯ�﷨
### SELECT IN ��ѯ
_ʹ��Dapper֧�ֵ��б����ת������_
``` C#
var sqlParam = new { userNameList = new string[] { 'love1','love2' } };
```
``` xml
<select id="selectInList">
 select * from user_info where user_name in @userNameList
</select>
```
_����ת��Ϊ��_
``` sql
select * from user_info where user_name in (@userNameList1,@userNameList2)
```

## 6.�����÷�
### �������� DbContext
```C#
public class CommonDbContext<TInsql> : DbContext where TInsql : class
{
	public CommonDbContext(CommonDbContextOptions<TInsql> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptions options)
	{
		var configuration = options.ServiceProvider.GetRequiredService<IConfiguration>();

		//TInsql type mapping to insql.xml type
		options.UseSqlResolver<TInsql>();

		options.UseSqlite(configuration.GetConnectionString("sqlite"));
	}
}

public class CommonDbContextOptions<TInsql> : DbContextOptions<CommonDbContext<TInsql>> where TInsql : class
{
	public CommonDbContextOptions(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}
}
```
### ���� Domain Service
```c#
public interface IUserService
{
    IEnumerable<UserInfo> GetUserList(string userName,Gender? userGender);
}
    
public class UserService : IUserService
{
    private readonly DbContext dbContext;

    //T is UserService
    public UserService(CommonDbContext<UserService> dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<UserInfo> GetUserList(string userName, Gender? userGender)
    {
        return this.dbContext.Query<UserInfo>(nameof(GetUserList), new { userName, userGender });
    }
}
```
### ���� Service.insql.xml
_���� `UserService.insql.xml` �ļ������޸�����ļ�������Ϊ`Ƕ��ʽ�ļ�`���� . `insql type` �� `UserService` ���Ͷ�Ӧ._
```xml
<insql type="Example.Domain.Services.UserService,Example.Domain" >
    <sql id="selectUserColumns">
    select user_id as UserId,user_name as UserName,user_gender as UserGender from user_info
    </sql>
    
    <select id="GetUserList">
    <include refid="selectUserColumns" />
    <where>
        <if test="userName != null">
        <bind name="likeUserName" value="'%' + userName + '%'" />
        user_name like @likeUserName
        </if>
        <if test="userGender != null ">
        and user_gender = @userGender
        </if>
    </where>
    order by  user_id
    </select>
</insql>
```
### ��� DbContext
```c#
public void ConfigureServices(IServiceCollection services)
{
	services.AddInsql();

	services.AddScoped(typeof(CommonDbContextOptions<>));
	services.AddScoped(typeof(CommonDbContext<>));

	services.AddScoped<IUserService, UserService>();
}
```
### ʹ�� Domain Service
```c#
public class ValuesController : ControllerBase
{
    private readonly IUserService userService;

    public ValuesController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        var list = this.userService.GetUserList("11", Domain.Gender.M);
	//todo return
    }
}
```  
