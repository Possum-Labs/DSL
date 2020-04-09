# Supported application controls

| Version  |   | 4                          | 5                  |   |
|----------|---|----------------------------|--------------------|---|
| TinyMCE  |   | :ballot_box_with_check:(1) | :white_check_mark: |   |
| CKEditor |   | :white_check_mark:         | :skull:(2)         |   |
|          |   |                            |                    |   |

###legend
:white_check_mark: Works
:ballot_box_with_check: Works with most xpath patterns
:skull: Not happening

## References
1. the app prepending the control prevents some xpaths from infering relationships as there is a diff in the way.
2. https://github.com/ckeditor/ckeditor5/issues/6554