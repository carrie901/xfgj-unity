//
//  WebViewController.h
//  Unity-iPhone
//
//  Created by 周 龙 on 14-1-7.
//
//

#import <UIKit/UIKit.h>

@interface RecommendViewController : UIViewController

@property (nonatomic) NSString *url;
@property (nonatomic) IBOutlet UIWebView *webView;

@end
