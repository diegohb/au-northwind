import { LogManager } from "aurelia-framework";
import { Interceptor } from "aurelia-fetch-client";

export class ApiLoggerInterceptor implements Interceptor {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    public request(requestParam: Request): Request | Response | Promise<Request | Response> {
        this._logger.debug(`Intercepted request using method: ${requestParam.method} with URL: ${requestParam.url}`);
        return requestParam;
    }

    public requestError(errorParam: any): Request | Response | Promise<Request | Response> {
        this._logger.debug(`Intercepted request error with details: ${errorParam}.`, errorParam);
        return errorParam;
    }

    public response(responseParam: Response, requestParam?: Request): Response | Promise<Response> {
        this._logger.debug(
            `Intercepted response ${responseParam.status} using URL: ${(requestParam ? requestParam : responseParam).url
            }`);
        return responseParam;
    }

    public responseError(errorParam: any, requestParam?: Request): Response | Promise<Response> {
        this._logger.debug(`[ERROR] Intercepted response error with details: ${errorParam}`, requestParam);
        return errorParam;
    }
}