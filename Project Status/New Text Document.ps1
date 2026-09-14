<#
.SYNOPSIS
    Extracts complete .NET/Next.js project structure and all source code to a single text file.
.DESCRIPTION
    Run this script in your project root folder. It creates "Complete-Project-Export.txt"
    containing the full directory tree and contents of all source files.
#>

$outputFile = "Complete-Project-Export.txt"

# Folders to exclude
$excludePatterns = @('\\bin\\', '\\obj\\', '\\.vs\\', '\\node_modules\\', '\\packages\\', '\\.git\\', '\\publish\\', '\\out\\', '\\.next\\', '\\dist\\', '\\coverage\\', '\\test-results\\')

# File extensions to include
$includeExtensions = @('.cs', '.csproj', '.sln', '.json', '.xml', '.config', '.razor', '.cshtml', '.http', '.md', '.yml', '.yaml', '.dockerfile', '.sh', '.ps1', '.js', '.ts', '.tsx', '.css', '.scss', '.html', '.env.example')

# Remove old export if exists
if (Test-Path $outputFile) { Remove-Item $outputFile }

# Collect files
$allFiles = Get-ChildItem -Recurse -File | Where-Object {
    $ext = $_.Extension.ToLower()
    $full = $_.FullName
    $extMatch = $includeExtensions -contains $ext
    $excluded = $false
    foreach ($p in $excludePatterns) {
        if ($full -match $p) { $excluded = $true; break }
    }
    $extMatch -and -not $excluded
} | Sort-Object FullName

# Build output
$lines = [System.Collections.Generic.List[string]]::new()
$lines.Add("=" * 80)
$lines.Add("PROJECT EXPORT - $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')")
$lines.Add("ROOT: $(Get-Location)")
$lines.Add("TOTAL FILES: $($allFiles.Count)")
$lines.Add("=" * 80)
$lines.Add("")

# Section 1: Tree
$lines.Add("#" * 80)
$lines.Add("SECTION 1: DIRECTORY TREE")
$lines.Add("#" * 80)
$lines.Add("")
$treeOutput = cmd /c "tree /F /A" 2>$null
if ($treeOutput) {
    foreach ($line in $treeOutput) {
        if ($line -notmatch '\\(bin|obj|\.vs|node_modules|packages)') {
            $lines.Add($line)
        }
    }
} else {
    $lines.Add("[tree command not available]")
}
$lines.Add("")

# Section 2: File contents
$lines.Add("#" * 80)
$lines.Add("SECTION 2: FILE CONTENTS")
$lines.Add("#" * 80)
$lines.Add("")

$counter = 0
foreach ($file in $allFiles) {
    $counter++
    $relativePath = Resolve-Path -Relative -Path $file.FullName
    $lines.Add("")
    $lines.Add("=" * 80)
    $lines.Add("FILE [$counter/$($allFiles.Count)]: $relativePath")
    $lines.Add("SIZE: $($file.Length) bytes")
    $lines.Add("=" * 80)

    try {
        $content = [System.IO.File]::ReadAllText($file.FullName, [System.Text.Encoding]::UTF8)
        if ([string]::IsNullOrWhiteSpace($content)) {
            $lines.Add("[EMPTY FILE]")
        } else {
            $lines.Add($content)
        }
    }
    catch {
        $lines.Add("[ERROR READING FILE: $_]")
    }
    $lines.Add("")
    $lines.Add("-" * 80)
}

# Write output with UTF-8 BOM
$utf8Bom = New-Object System.Text.UTF8Encoding $true
[System.IO.File]::WriteAllLines($outputFile, $lines, $utf8Bom)

Write-Host ""
Write-Host "Export Complete!" -ForegroundColor Green
Write-Host "Saved: $((Resolve-Path $outputFile).Path)" -ForegroundColor Cyan
Write-Host "Files: $($allFiles.Count)" -ForegroundColor Yellow
Write-Host "Size: $([math]::Round((Get-Item $outputFile).Length / 1KB, 2)) KB" -ForegroundColor Yellow
Write-Host ""
Write-Host "SECURITY WARNING - Check before sharing:" -ForegroundColor Red
Write-Host "  - Connection strings in appsettings*.json" -ForegroundColor Yellow
Write-Host "  - API Keys, Secrets, Passwords" -ForegroundColor Yellow
Write-Host "  - Personal/Company sensitive data" -ForegroundColor Yellow
