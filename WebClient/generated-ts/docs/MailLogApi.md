# MailLogApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**mailLogGet**](MailLogApi.md#maillogget) | **GET** /mail-log |  |
| [**mailLogIdGet**](MailLogApi.md#maillogidget) | **GET** /mail-log/{id} |  |



## mailLogGet

> Array&lt;MailLog&gt; mailLogGet()



### Example

```ts
import {
  Configuration,
  MailLogApi,
} from '';
import type { MailLogGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const config = new Configuration({ 
    // To configure API key authorization: Bearer
    apiKey: "YOUR API KEY",
  });
  const api = new MailLogApi(config);

  try {
    const data = await api.mailLogGet();
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters

This endpoint does not need any parameter.

### Return type

[**Array&lt;MailLog&gt;**](MailLog.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)


## mailLogIdGet

> MailLog mailLogIdGet(id)



### Example

```ts
import {
  Configuration,
  MailLogApi,
} from '';
import type { MailLogIdGetRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const config = new Configuration({ 
    // To configure API key authorization: Bearer
    apiKey: "YOUR API KEY",
  });
  const api = new MailLogApi(config);

  const body = {
    // string
    id: 38400000-8cf0-11bd-b23e-10b96e4ef00d,
  } satisfies MailLogIdGetRequest;

  try {
    const data = await api.mailLogIdGet(body);
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}

// Run the test
example().catch(console.error);
```

### Parameters


| Name | Type | Description  | Notes |
|------------- | ------------- | ------------- | -------------|
| **id** | `string` |  | [Defaults to `undefined`] |

### Return type

[**MailLog**](MailLog.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

