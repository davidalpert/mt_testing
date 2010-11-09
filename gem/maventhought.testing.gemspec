version = File.read(File.expand_path("../VERSION",__FILE__)).strip  
  
Gem::Specification.new do |spec|  
  spec.platform    = Gem::Platform::CURRENT  
  spec.name        = 'maventhought.testing'  
  spec.version     = version  
  spec.files = Dir['lib/**/*'] + Dir['doc/**/*']
  
  spec.summary     = 'Testing Framework with automocking dependencies'  
  spec.description = 'Base classes to use for testing (based on MbUnit) that enforce Given, When, Then style with auto mocking facilities'  
    
  spec.authors           = ['Amir Barylko']  
  spec.email             = 'amir@maventhought.com'  
  spec.homepage          = 'http://maventhought.com'  
  spec.rubyforge_project = 'maventhought.testing'  
end  