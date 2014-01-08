//
//  IosPlugin.m
//  Unity-iPhone
//
//  Created by 周 龙 on 13-12-16.
//
//
#import "IosPlugin.h"
#import "WebViewController.h"
#import "UnityAppController.h"

@implementation IosPlugin


+ (UIViewController*) getCurrentRootViewController {
    UIViewController *result;

    UIWindow *topWindow = [[UIApplication sharedApplication] keyWindow];
    if (topWindow.windowLevel != UIWindowLevelNormal)
    {
        NSArray *windows = [[UIApplication sharedApplication] windows];
        for(topWindow in windows)
        {
            if (topWindow.windowLevel == UIWindowLevelNormal)
                break;
        }
    }
        
    UIView *rootView = [[topWindow subviews] objectAtIndex:0];
    id nextResponder = [rootView nextResponder];
        
    if ([nextResponder isKindOfClass:[UIViewController class]])
        result = nextResponder;
    else if ([topWindow respondsToSelector:@selector(rootViewController)] && topWindow.rootViewController != nil)
        result = topWindow.rootViewController;
    else
        NSAssert(NO, @"ShareKit: Could not find a root view controller.  You can assign one manually by calling [[SHK currentHelper] setRootViewController:YOURROOTVIEWCONTROLLER].");
    return result;
}


@end

// Converts C style string to NSString
NSString* CreateNSString (const char* string) {
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}

// Helper method to create C string copy
char* MakeStringCopy (const char* string) {
	if (string == NULL)
		return NULL;
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy(res, string);
	return res;
}

extern "C" {
    
	char* GetBundleVersion() {
        NSString* version = [[[NSBundle mainBundle] infoDictionary] objectForKey:(NSString *)kCFBundleVersionKey];
        return MakeStringCopy([version UTF8String]);
    }
    
    void OpenWebsite(const char* url) {
        NSLog(@"%@", CreateNSString(url));
        WebViewController *webViewController = [[WebViewController alloc] initWithNibName:@"WebView" bundle:nil];
        /*UnityAppController *appController = (UnityAppController*)[UIApplication sharedApplication].delegate;
        [appController.window addSubview:webViewController.view];
        [webViewController.webView loadHTMLString:nil baseURL:[NSURL URLWithString:@"http://www.baidu.com"]];*/
        [[UIApplication sharedApplication].keyWindow.rootViewController presentViewController:webViewController animated:YES completion:nil];
    }
	
}
