namespace backend.Communication.Responses;

public class BaseResponse
{
    public int ResponseCode { get; set; } = 200;
    public string ErrorMessage { get; set; }
    public string ErrorCode { get; set; }
    
    
    public static BaseResponse Ok
    {
        get { return new BaseResponse { ResponseCode = 200, }; }
    }

    public static BaseResponse Error(string errorCode, string message=null)
    {
        if (message == null)
            message = errorCode;

        return new BaseResponse { ResponseCode = 200, ErrorCode= errorCode, ErrorMessage=message };
    }
}