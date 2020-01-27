Feature: Script Based Clear
	using javascript driven clear on '//div[@id="sandbox-container"]/input'

Scenario Outline: datepicker external enter and clear
	Given navigated to '<url>'
	When entering '<entry>' into element '//div[@id="sandbox-container"]/input'
	And clicking the element '#sandbox-html'
	And using javascript driven clear on '//div[@id="sandbox-container"]/input'
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

Scenario Outline: datepicker external enter and clear version 2
	Given navigated to '<url>'
	When clicking the element 'Switch to Bootstrap 2'
	And entering '<entry>' into element '//div[@id="sandbox-container"]/input'
	And clicking the element '#sandbox-html'
	And using javascript driven clear on '//div[@id="sandbox-container"]/input'
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

Scenario Outline: external enter and clear
	Given navigated to 'https://igorescobar.github.io/jQuery-Mask-Plugin/'
	When entering '<entry>' into element '<id>'
	When using javascript driven clear on '<id>'
	Then the element '<id>' has the value ''
Examples:
| name                           | id                  | entry          | 
| Date                           | #date               | 11112000       | 
| Date             zero padded   | #date               | 01012000       | 
| Hour                           | #time               | 111111         | 
| Date & Hour                    | #date_time          | 11112000111111 | 
| ZIP Code                       | #cep                | 80202          | 
| With Callbacks (open console)  | #cep_with_callback  | 80202          | 
| Crazy Zip Code                 | #crazy_cep          | 8020211        | 
| Money                          | #money              | 123456         | 
| Mask placeholder option        | #placeholder        | 01012000       | 
| Telephone                      | #phone              | 1234567        | 
| Telephone with Code Area       | #phone_with_ddd     | 1234567890     | 
| US Telephone                   | #phone_us           | 1234567890     | 
| São Paulo Celphones            | #sp_celphones       | 1234567890     | 
| Mixed Type Mask                | #mixed              | 123456a        | 
| CPF                            | #cpf                | 1234567890     | 
| CNPJ                           | #cnpj               | 1234567890     | 
| IP Address                     | #ip_address         | 123123123123   | 
| With Clear If Not Match Option | #clear-if-not-match | 01012000       | 
| With a fallback digit          | #fallback           | 01012000       | 
| With selectOnFocus             | #selectonfocus      | 01012000       | 