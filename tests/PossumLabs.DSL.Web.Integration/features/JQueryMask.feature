Feature: JQueryMask

Scenario Outline: external enter
	Given navigated to 'https://igorescobar.github.io/jQuery-Mask-Plugin/'
	When entering '<entry>' into element '<id>'
	Then the element '<id>' has the value '<data>'
Examples:
| name                           | id                  | entry          | data                |
| Date                           | #date               | 11112000       | 11/11/2000          |
| Date             zero padded   | #date               | 01012000       | 01/01/2000          |
| Hour                           | #time               | 111111         | 11:11:11            |
| Date & Hour                    | #date_time          | 11112000111111 | 11/11/2000 11:11:11 |
| ZIP Code                       | #cep                | 80202          | 80202               |
| With Callbacks (open console)  | #cep_with_callback  | 80202          | 80202               |
| Crazy Zip Code                 | #crazy_cep          | 8020211        | 8-02-02-11          |
#| Money                          | #money              | 123456         | 1.234,56            | core bug
| Mask placeholder option        | #placeholder        | 01012000       | 01/01/2000          |
| Telephone                      | #phone              | 12345678        | 1234-5678           |
| Telephone with Code Area       | #phone_with_ddd     | 1234567890     | (12) 3456-7890      |
| US Telephone                   | #phone_us           | 1234567890     | (123) 456-7890      |
| São Paulo Celphones            | #sp_celphones       | 1234567890     | (12) 3456-7890      |
| Mixed Type Mask                | #mixed              | 123456a        | 123 456-a           |
| CPF                            | #cpf                | 1234567890     | 12.345.678-90       |
| CNPJ                           | #cnpj               | 1234567890     | 1.234/5678-90       |
#| IP Address                     | #ip_address         | 123123123123   | 123.123.123.123     | core bug
| With Clear If Not Match Option | #clear-if-not-match | 01012000       | 01/01/2000          |
| With a fallback digit          | #fallback           | 01012000       | 01/01/2000          |
| With selectOnFocus             | #selectonfocus      | 01012000       | 01/01/2000          |

Scenario Outline: external enter and clear
	Given navigated to 'https://igorescobar.github.io/jQuery-Mask-Plugin/'
	When entering '<entry>' into element '<id>'
	When entering '' into element '<id>'
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