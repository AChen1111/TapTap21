# GravitySystem

* Namespace: GamePlay.Gravity

* Script Location: Assets/Scripts/Common/Gravity

  

  ### 重力系统：

| 类别              |               说明               |
| :---------------- | :------------------------------: |
| GravityService    |    全局单例，管理全局重力效果    |
| GravityBody       | 重力受体，将重力效果作用于物体上 |
| EGravityDirection |           枚举重力方向           |

### EGravityDirection:

| 枚举 |  值  |
| :--: | :--: |
| Down |  1   |
|  Up  |  -1  |

### GravityService:

通过 GravityService 调用以下方法来改变重力：

| 方法                                 |        参数名         |                         作用 |
| ------------------------------------ | :-------------------: | ---------------------------: |
| SetGravity(EGravityDirection)        |      gDirection       |     设置重力方向为gDirection |
| SetGravity(float)                    |       gStrength       |      设置重力大小为gStrength |
| SetGravity(EGravityDirection, float) | gDirection, gStrength |           设置重力大小及方向 |
| Flip()                               |          ---          |                 翻转重力方向 |
| ResetGravity()                       |          ---          | 重置重力方向为向下，大小为 1 |

GravityService 派发以下内容：

| 广播           | 条件                           | 传参                         |
| -------------- | ------------------------------ | ---------------------------- |
| GravityChanged | 当重力大小或方向发生改变时发送 | *重力乘积因子* 与 *重力方向* |
| GravityFlipped | 仅当重力方向发生改变时发送     | *重力方向*                   |

- 广播顺序：GravityFlipped (If happened) -> GravityChanged
- 重力乘积因子 以乘积的方式作用于原物体的 GravityScale 上（若原物体 GravityScale 为负，则当重力方向为 Up 的时候物体受向下的重力，重力大小同理）
- *IgnoreIfUnchanged* 参数默认开启，启用后，仅当重力大小或方向发生改变时发送广播

### GravityBody:

重力效果的接收端，需挂载到带有 Rigidbody2D 的 gameObject 上。

| 参数  |           作用            |
| ----- | :-----------------------: |
| FlipX | 当重力翻转时沿X轴翻转物体 |
| FlipY | 当重力翻转时沿Y轴翻转物体 |

* 翻转效果作用于物体的 transform 上，该操作会翻转物体的子物体