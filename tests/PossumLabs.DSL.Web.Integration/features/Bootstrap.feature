Feature: Bootstrap
https://uxsolutions.github.io/bootstrap-datepicker/
using And clicking the element '#sandbox-html' to remove focus and trigger javascript

Scenario Outline: datepicker external enter
	Given navigated to '<url>'
	When entering '<entry>' into element '//div[@id="sandbox-container"]/input'
	And clicking the element '#sandbox-html'
	Then the element '//div[@id="sandbox-container"]/input' has the value '<data>'
Examples:
| name                     | entry      | data       | xpath                                  | url                                                                                                                                                                                                                                                                                                    |
| Text input               | 11/11/2000 | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox                  |
| Component                | 11/11/2000 | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox              |
| Text input autoclose     | 11/11/2000 | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&keyboardNavigation=on&forceParse=on#sandbox     |
| Component autoclose      | 11/11/2000 | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&KeyboardNavigation=on&forceParse=on#sandbox |
| Text input no forceparse | 11/11/2000 | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on#sandbox                                |
| Component no forceparse  | 11/11/2000 | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&KeyboardNavigation=on#sandbox                            |


Scenario Outline: datepicker external enter and clear
	Given navigated to '<url>'
	When entering '<entry>' into element '//div[@id="sandbox-container"]/input'
	And clicking the element '#sandbox-html'
	And entering '' into element '//div[@id="sandbox-container"]/input'
	And clicking the element '#sandbox-html'
	Then the element '//div[@id="sandbox-container"]/input' has the value ''
Examples:
| name                     | entry      | xpath                                  | url                                                                                                                                                                                                                                                                                                    |
| Text input               | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox                  |
| Component                | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox              |
| Text input autoclose     | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&keyboardNavigation=on&forceParse=on#sandbox     |
| Component autoclose      | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&KeyboardNavigation=on&forceParse=on#sandbox |
| Text input no forceparse | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on#sandbox                                |
| Component no forceparse  | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&KeyboardNavigation=on#sandbox                            |


Scenario Outline: datepicker external enter version 2
	Given navigated to '<url>'
	When clicking the element 'Switch to Bootstrap 2'
	And entering '<entry>' into element '//div[@id="sandbox-container"]/input'
	And clicking the element '#sandbox-html'
	Then the element '//div[@id="sandbox-container"]/input' has the value '<data>'
Examples:
| name                     | entry      | data       | xpath                                  | url                                                                                                                                                                                                                                                                                                    |
| Text input               | 11/11/2000 | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox                  |
| Component                | 11/11/2000 | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox              |
| Text input autoclose     | 11/11/2000 | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&KeyboardNavigation=on&forceParse=on#sandbox     |
| Component autoclose      | 11/11/2000 | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&KeyboardNavigation=on&forceParse=on#sandbox |
| Text input no forceparse | 11/11/2000 | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&KeyboardNavigation=on#sandbox                                |
| Component no forceparse  | 11/11/2000 | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&KeyboardNavigation=on#sandbox                            |


Scenario Outline: datepicker external enter and clear version 2
	Given navigated to '<url>'
	When clicking the element 'Switch to Bootstrap 2'
	And entering '<entry>' into element '//div[@id="sandbox-container"]/input'
	And clicking the element '#sandbox-html'
	And entering '' into element '//div[@id="sandbox-container"]/input'
	And clicking the element '#sandbox-html'
	Then the element '//div[@id="sandbox-container"]/input' has the value ''
Examples:
| name                     | entry      | xpath                                  | url                                                                                                                                                                                                                                                                                                    |
| Text input               | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox                  |
| Component                | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox              |
| Text input autoclose     | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&KeyboardNavigation=on&forceParse=on#sandbox     |
| Component autoclose      | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&KeyboardNavigation=on&forceParse=on#sandbox |
| Text input no forceparse | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&KeyboardNavigation=on#sandbox                                |
| Component no forceparse  | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&KeyboardNavigation=on#sandbox                            |




