# MailApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|------------- | ------------- | -------------|
| [**mailSendPost**](MailApi.md#mailsendpost) | **POST** /mail/send |  |



## mailSendPost

> SendMailResponse mailSendPost(sendMailRequest)



### Example

```ts
import {
  Configuration,
  MailApi,
} from '';
import type { MailSendPostRequest } from '';

async function example() {
  console.log("🚀 Testing  SDK...");
  const config = new Configuration({ 
    // To configure API key authorization: Bearer
    apiKey: "YOUR API KEY",
  });
  const api = new MailApi(config);

  const body = {
    // SendMailRequest (optional)
    sendMailRequest: ...,
  } satisfies MailSendPostRequest;

  try {
    const data = await api.mailSendPost(body);
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
| **sendMailRequest** | [SendMailRequest](SendMailRequest.md) |  | [Optional] |

### Return type

[**SendMailResponse**](SendMailResponse.md)

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

