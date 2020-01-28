Feature: Script based set
	using javascript driven clear on '//div[@id="sandbox-container"]/input'

Scenario Outline: datepicker external enter and clear
	Given navigated to '<url>'
	When using javascript setting '<entry>' for element '//div[@id="sandbox-container"]/input'
	Then the element '//div[@id="sandbox-container"]/input' has the value '<entry>'
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
	And using javascript setting '<entry>' for element '//div[@id="sandbox-container"]/input'
	Then the element '//div[@id="sandbox-container"]/input' has the value '<entry>'
Examples:
| name                     | entry      | xpath                                  | url                                                                                                                                                                                                                                                                                                    |
| Text input               | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox                  |
| Component                | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&keyboardNavigation=on&forceParse=on#sandbox              |
| Text input autoclose     | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&KeyboardNavigation=on&forceParse=on#sandbox     |
| Component autoclose      | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&autoclose=on&KeyboardNavigation=on&forceParse=on#sandbox |
| Text input no forceparse | 11/11/2000 | //div[@id="sandbox-container"]/input   | https://uxsolutions.github.io/bootstrap-datepicker/?markup=input&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&KeyboardNavigation=on#sandbox                                |
| Component no forceparse  | 11/11/2000 | //*[@id="sandbox-container"]/div/input | https://uxsolutions.github.io/bootstrap-datepicker/?markup=component&format=&weekStart=&startDate=&endDate=&startView=0&minViewMode=0&maxViewMode=4&todayBtn=false&clearBtn=false&language=en&orientation=auto&multidate=&multidateSeparator=&KeyboardNavigation=on#sandbox                            |

Scenario Outline: external enter
	Given navigated to 'https://igorescobar.github.io/jQuery-Mask-Plugin/'
	When using javascript setting '<entry>' for element '<id>'
	Then the element '<id>' has the value '<expected>'
Examples:
| name                           | id                  | entry               | expected            |
| Date                           | #date               | 11/11/2000          | 11/11/2000          |
| Date             zero padded   | #date               | 01/01/2000          | 01/01/2000          |
| Hour                           | #time               | 11:11:11            | 11:11:11            |
| Date & Hour                    | #date_time          | 11/11/2000 11:11:11 | 11/11/2000 11:11:11 |
| ZIP Code                       | #cep                | 80202               | 80202               |
| With Callbacks (open console)  | #cep_with_callback  | 80202               | 80202               |
| Crazy Zip Code                 | #crazy_cep          | 8020211             | 8020211             |
| Money                          | #money              | 123456              | 123456              |
| Mask placeholder option        | #placeholder        | 01012000            | 01012000            |
| Telephone                      | #phone              | 1234567             | 1234567             |
| Telephone with Code Area       | #phone_with_ddd     | 1234567890          | 1234567890          |
| US Telephone                   | #phone_us           | 1234567890          | 1234567890          |
| São Paulo Celphones            | #sp_celphones       | 1234567890          | 1234567890          |
| Mixed Type Mask                | #mixed              | 123456a             | 123456a             |
| CPF                            | #cpf                | 1234567890          | 1234567890          |
| CNPJ                           | #cnpj               | 1234567890          | 1234567890          |
| IP Address                     | #ip_address         | 123123123123        | 123123123123        |
| With Clear If Not Match Option | #clear-if-not-match | 01012000            | 01012000            |
| With a fallback digit          | #fallback           | 01012000            | 01012000            |
| With selectOnFocus             | #selectonfocus      | 01012000            | 01012000            |