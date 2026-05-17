# ClientAppApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**clientAppRegisterPost**](ClientAppApi.md#clientappregisterpost) | **POST** /client-app/register |  |



## clientAppRegisterPost

> RegisterClientAppResponse clientAppRegisterPost(registerClientAppRequest)



### Example

```ts
import {
  Configuration,
  ClientAppApi,
} from '';
import type { ClientAppRegisterPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const config = new Configuration({ 
    // To configure API key authorization: Bearer
    apiKey: "YOUR API KEY",
  });
  const api = new ClientAppApi(config);

  const body = {
    // RegisterClientAppRequest (optional)
    registerClientAppRequest: ...,
  } satisfies ClientAppRegisterPostRequest;

  try {
    const data = await api.clientAppRegisterPost(body);
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
| **registerClientAppRequest** | [RegisterClientAppRequest](RegisterClientAppRequest.md) |  | [Optional] |

### Return type

[**RegisterClientAppResponse**](RegisterClientAppResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#api-endpoints) [[Back to Model list]](../README.md#models) [[Back to README]](../README.md)

